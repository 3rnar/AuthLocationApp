using AuthLocationApp.Application.Interfaces.Services;

namespace AuthLocationApp.Infrastructure.Services
{
   public class BCryptPasswordHasher : IPasswordHasher
   {
      public string HashPassword(string plainPassword)
      {
         return BCrypt.Net.BCrypt.HashPassword(plainPassword);
      }

      public bool VerifyPassword(string plainPassword, string hashedPassword)
      {
         return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
      }
   }
}
