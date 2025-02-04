using Microsoft.EntityFrameworkCore;
using AuthLocationApp.Application.Interfaces.Repositories;
using AuthLocationApp.Domain;
using AuthLocationApp.Infrastructure.Data;
using AuthLocationApp.Infrastructure.DbModels;
using AuthLocationApp.Infrastructure.Mappers;
using Serilog;

namespace AuthLocationApp.Infrastructure.Repositories
{
   internal class ProvinceRepository : BaseRepository<Province, ProvinceDbModel>, IProvinceRepository
   {
      private readonly ILogger _logger;

      public ProvinceRepository(ApplicationDbContext context, IMapper<ProvinceDbModel, Province> mapper)
          : base(context, mapper)
      {
         _logger = Log.ForContext<ProvinceRepository>();
      }

      public async Task<Province?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
      {
         _logger.Information("Fetching province by name: {Name}", name);

         try
         {
            var province = await _dbSet
                .Where(p => p.Name == name)
                .Select(p => _mapper.ToDomain(p))
                .FirstOrDefaultAsync(cancellationToken);

            if (province is null)
            {
               _logger.Warning("Province with name {Name} not found", name);
            }
            else
            {
               _logger.Information("Province with name {Name} retrieved successfully", name);
            }

            return province;
         }
         catch (Exception ex)
         {
            _logger.Error(ex, "Error fetching province by name: {Name}", name);
            throw;
         }
      }

      public async Task<IReadOnlyList<Province>> GetAllByCountryIdAsync(int countryId, CancellationToken cancellationToken = default)
      {
         _logger.Information("Fetching all provinces for country ID: {CountryId}", countryId);

         try
         {
            var provinces = await _dbSet
                .AsNoTracking()
                .Where(p => p.CountryId == countryId)
                .ToListAsync(cancellationToken);

            var result = provinces.Select(p => _mapper.ToDomain(p)).ToList().AsReadOnly();

            _logger.Information("{Count} provinces retrieved for country ID {CountryId}", result.Count, countryId);
            return result;
         }
         catch (Exception ex)
         {
            _logger.Error(ex, "Error fetching provinces for country ID: {CountryId}", countryId);
            throw;
         }
      }
   }
}
