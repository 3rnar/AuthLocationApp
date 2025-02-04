using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLocationApp.Application.Interfaces.Repositories
{
   public interface IBaseRepository<TDomain> where TDomain : class
   {
      Task<IReadOnlyCollection<TDomain>> GetAllAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);
      Task<TDomain?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
      Task AddAsync(TDomain entity, CancellationToken cancellationToken = default);
      void Update(TDomain entity);
      void Delete(TDomain entity);
      Task SaveChangesAsync(CancellationToken cancellationToken = default);
   }
}
