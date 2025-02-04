using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthLocationApp.Infrastructure.DbModels;

namespace AuthLocationApp.Infrastructure.Configurations
{
   public class ProvinceDbModelConfiguration : IEntityTypeConfiguration<ProvinceDbModel>
   {
      public void Configure(EntityTypeBuilder<ProvinceDbModel> builder)
      {
         builder.HasKey(p => p.Id);

         builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

         builder.HasOne(p => p.Country)
                .WithMany(c => c.Provinces)
                .HasForeignKey(p => p.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

         builder.HasMany(p => p.Users)
                .WithOne(u => u.Province)
                .HasForeignKey(u => u.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);
      }
   }
}
