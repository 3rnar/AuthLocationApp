using AuthLocationApp.Domain;
using AuthLocationApp.Infrastructure.DbModels;

namespace AuthLocationApp.Infrastructure.Mappers
{
   public class CountryMapper : IMapper<CountryDbModel, Country>
   {
      public Country ToDomain(CountryDbModel dbModel)
      {
         var domainCountry = new Country(dbModel.Id, dbModel.Name);
         if (dbModel.Provinces != null && dbModel.Provinces.Any())
         {
            foreach (var provinceDb in dbModel.Provinces)
            {
               var provinceDomain = new Province(provinceDb.Id, provinceDb.Name, provinceDb.CountryId);
               domainCountry.AddProvince(provinceDomain);
            }
         }
         return domainCountry;
      }

      public CountryDbModel ToDbModel(Country domain)
      {
         return new CountryDbModel
         {
            Id = domain.Id,
            Name = domain.Name,
            Provinces = domain.Provinces.Select(p => new ProvinceDbModel
            {
               Id = p.Id,
               Name = p.Name,
               CountryId = p.CountryId
            }).ToList()
         };
      }
   }
}
