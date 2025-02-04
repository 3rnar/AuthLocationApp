using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using AuthLocationApp.Infrastructure.Data;
using AuthLocationApp.Infrastructure.DbModels;

namespace AuthLocationApp.Infrastructure.Seed
{
   public static class DbInitializer
   {
      public static async Task SeedAsync(ApplicationDbContext context)
      {
         await context.Database.MigrateAsync();

         await AddCountriesAsync(context);
         await AddProvincesAsync(context);

         await context.SaveChangesAsync();
      }

      private static async Task AddCountriesAsync(ApplicationDbContext context)
      {
         var countries = new List<CountryDbModel>
            {
                new() { Name = "USA" },
                new() { Name = "Canada" }
            };

         foreach (var country in countries)
         {
            var existing = await context.Countries.AnyAsync(x => x.Name == country.Name);
            if (!existing)
            {
               await context.Countries.AddAsync(country);
            }
         }
      }

      private static async Task AddProvincesAsync(ApplicationDbContext context)
      {
         var provinces = new List<ProvinceDbModel>
            {
                new() { Name = "California", CountryId = 1 },
                new() { Name = "Texas", CountryId = 1 },
                new() { Name = "New York", CountryId = 1 },
                new() { Name = "Ontario", CountryId = 2 },
                new() { Name = "British Columbia", CountryId = 2 },
                new() { Name = "Quebec", CountryId = 2 }
            };

         foreach (var province in provinces)
         {
            var existing = await context.Provinces.AnyAsync(p => p.Name == province.Name && p.CountryId == province.CountryId);

            if (!existing)
            {
               await context.Provinces.AddAsync(province);
            }
         }
      }
   }
}
