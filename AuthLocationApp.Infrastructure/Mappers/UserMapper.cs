using AuthLocationApp.Domain;
using AuthLocationApp.Infrastructure.DbModels;

namespace AuthLocationApp.Infrastructure.Mappers
{
   public class UserMapper : IMapper<UserDbModel, User>
   {
      public User ToDomain(UserDbModel dbModel)
      {
         var domainUser = new User(dbModel.Id, dbModel.Email, dbModel.PasswordHash, dbModel.CountryId, dbModel.ProvinceId);
         return domainUser;
      }

      public UserDbModel ToDbModel(User domain)
      {
         return new UserDbModel
         {
            Id = domain.Id,
            Email = domain.Email.ToString(),
            PasswordHash = domain.PasswordHash,
            CountryId = domain.CountryId,
            ProvinceId = domain.ProvinceId
         };
      }
   }
}
