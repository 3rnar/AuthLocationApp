using AuthLocationApp.Domain;
using AuthLocationApp.Infrastructure.DbModels;

namespace AuthLocationApp.Infrastructure.Mappers
{
   public class ProvinceMapper : IMapper<ProvinceDbModel, Province>
   {
      public Province ToDomain(ProvinceDbModel dbModel)
      {
         var domainProvince = new Province(dbModel.Id, dbModel.Name, dbModel.CountryId);
         return domainProvince;
      }

      public ProvinceDbModel ToDbModel(Province domain)
      {
         return new ProvinceDbModel
         {
            Id = domain.Id,
            Name = domain.Name,
            CountryId = domain.CountryId
         };
      }
   }
}
