using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthLocationApp.Infrastructure.DbModels;

namespace AuthLocationApp.Infrastructure.Configurations
{
   internal class UserDbModelConfiguration : IEntityTypeConfiguration<UserDbModel>
   {
      public void Configure(EntityTypeBuilder<UserDbModel> builder)
      {
         builder.HasKey(u => u.Id);

         builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

         builder.Property(u => u.PasswordHash)
                .IsRequired();

         builder.HasOne(u => u.Country)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

         builder.HasOne(u => u.Province)
                .WithMany(p => p.Users)
                .HasForeignKey(u => u.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);
      }
   }
}
