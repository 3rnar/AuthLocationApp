using AuthLocationApp.Domain;

namespace AuthLocationApp.Application.Interfaces.Repositories
{
   public interface IProvinceRepository : IBaseRepository<Province>
   {
      Task<Province?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
      Task<IReadOnlyList<Province>> GetAllByCountryIdAsync(int countryId, CancellationToken cancellationToken = default);
   }
}
