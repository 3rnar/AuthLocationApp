using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthLocationApp.Infrastructure.DbModels;

namespace AuthLocationApp.Infrastructure.Configurations
{
   public class CountryDbModelConfiguration : IEntityTypeConfiguration<CountryDbModel>
   {
      public void Configure(EntityTypeBuilder<CountryDbModel> builder)
      {
         builder.HasKey(c => c.Id);

         builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

         builder.HasMany(c => c.Provinces)
                .WithOne(p => p.Country)
                .HasForeignKey(p => p.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

         builder.HasMany(c => c.Users)
                .WithOne(u => u.Country)
                .HasForeignKey(u => u.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
      }
   }
}
