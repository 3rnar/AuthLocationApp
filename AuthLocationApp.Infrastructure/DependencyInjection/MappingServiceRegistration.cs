using Microsoft.Extensions.DependencyInjection;
using AuthLocationApp.Domain;
using AuthLocationApp.Infrastructure.DbModels;
using AuthLocationApp.Infrastructure.Mappers;

namespace AuthLocationApp.Infrastructure.DependencyInjection
{
   public static class MappingServiceRegistration
   {
      public static IServiceCollection AddMappingServices(this IServiceCollection services)
      {
         services.AddSingleton<IMapper<UserDbModel, User>, UserMapper>();
         services.AddSingleton<IMapper<CountryDbModel, Country>, CountryMapper>();
         services.AddSingleton<IMapper<ProvinceDbModel, Province>, ProvinceMapper>();
         return services;
      }
   }
}
