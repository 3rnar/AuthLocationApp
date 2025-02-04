using AuthLocationApp.Domain;

namespace AuthLocationApp.Application.Interfaces.Repositories
{
   public interface IUserRepository : IBaseRepository<User>
   {
      Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
   }
}
