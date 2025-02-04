using Microsoft.EntityFrameworkCore;
using AuthLocationApp.Infrastructure.Configurations;
using AuthLocationApp.Infrastructure.DbModels;

namespace AuthLocationApp.Infrastructure.Data
{
   public class ApplicationDbContext : DbContext
   {
      public DbSet<UserDbModel> Users { get; set; } = null!;
      public DbSet<CountryDbModel> Countries { get; set; } = null!;
      public DbSet<ProvinceDbModel> Provinces { get; set; } = null!;

      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.ApplyConfiguration(new UserDbModelConfiguration());
         modelBuilder.ApplyConfiguration(new CountryDbModelConfiguration());
         modelBuilder.ApplyConfiguration(new ProvinceDbModelConfiguration());

         base.OnModelCreating(modelBuilder);
      }
   }
}
