using Microsoft.EntityFrameworkCore;
using AuthLocationApp.Application.Interfaces.Repositories;
using AuthLocationApp.Domain;
using AuthLocationApp.Infrastructure.Data;
using AuthLocationApp.Infrastructure.DbModels;
using AuthLocationApp.Infrastructure.Mappers;
using Serilog;

namespace AuthLocationApp.Infrastructure.Repositories
{
   public class UserRepository : BaseRepository<User, UserDbModel>, IUserRepository
   {
      public UserRepository(ApplicationDbContext context, IMapper<UserDbModel, User> mapper)
          : base(context, mapper)
      {
      }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
        {
            _logger.Information("Checking if user exists with email: {Email}", email);

            try
            {
                var exists = await _dbSet
                    .AnyAsync(u => u.Email == email, cancellationToken);

                if (exists)
                {
                    _logger.Information("User with email {Email} exists.", email);
                }
                else
                {
                    _logger.Warning("No user found with email {Email}.", email);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error checking if user exists by email: {Email}", email);
                throw;
            }
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
      {
         _logger.Information("Fetching user by email: {Email}", email);

         try
         {
            var user = await _dbSet
                .Where(u => u.Email == email)
                .Select(u => _mapper.ToDomain(u))
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
            {
               _logger.Warning("User with email {Email} not found", email);
            }
            else
            {
               _logger.Information("User with email {Email} retrieved successfully", email);
            }

            return user;
         }
         catch (Exception ex)
         {
            _logger.Error(ex, "Error fetching user by email: {Email}", email);
            throw;
         }
      }
   }
}
