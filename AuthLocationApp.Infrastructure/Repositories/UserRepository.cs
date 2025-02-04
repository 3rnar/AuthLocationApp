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
      private readonly ILogger _logger;

      public UserRepository(ApplicationDbContext context, IMapper<UserDbModel, User> mapper)
          : base(context, mapper)
      {
         _logger = Log.ForContext<UserRepository>();
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
