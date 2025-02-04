using AuthLocationApp.Domain;

namespace AuthLocationApp.Application.Interfaces.Repositories
{
   public interface ICountryRepository : IBaseRepository<Country>
   {
      Task<Country?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
   }
}
