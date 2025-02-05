using Microsoft.EntityFrameworkCore;
using AuthLocationApp.Application.Interfaces.Repositories;
using AuthLocationApp.Domain;
using AuthLocationApp.Infrastructure.Data;
using AuthLocationApp.Infrastructure.DbModels;
using AuthLocationApp.Infrastructure.Mappers;
using Serilog;

namespace AuthLocationApp.Infrastructure.Repositories
{
   public class CountryRepository : BaseRepository<Country, CountryDbModel>, ICountryRepository
   {
      public CountryRepository(ApplicationDbContext context, IMapper<CountryDbModel, Country> mapper)
          : base(context, mapper)
      {
      }

      public async Task<Country?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
      {
         _logger.Information("Fetching country by name: {Name}", name);

         try
         {
            var country = await _dbSet
                .Where(c => c.Name == name)
                .Select(c => _mapper.ToDomain(c))
                .FirstOrDefaultAsync(cancellationToken);

            if (country is null)
            {
               _logger.Warning("Country with name {Name} not found", name);
            }
            else
            {
               _logger.Information("Country with name {Name} retrieved successfully", name);
            }

            return country;
         }
         catch (Exception ex)
         {
            _logger.Error(ex, "Error fetching country by name: {Name}", name);
            throw;
         }
      }
   }
}
