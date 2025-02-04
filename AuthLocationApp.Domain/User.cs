using AuthLocationApp.Domain.Exceptions;
using AuthLocationApp.Domain.ValueObjects;

namespace AuthLocationApp.Domain
{
   public class User
   {
      public int Id { get; private set; }
      public EmailAddress Email { get; private set; }
      public string PasswordHash { get; private set; }
      public int CountryId { get; private set; }
      public int ProvinceId { get; private set; }

      public User(int id, string email, string passwordHash, int countryId, int provinceId)
      {
         Email = new EmailAddress(email);

         if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainException("Password hash cannot be empty.");

         if (countryId <= 0)
            throw new DomainException("CountryId must be greater than zero.");

         if (provinceId <= 0)
            throw new DomainException("ProvinceId must be greater than zero.");

         Id = id;
         PasswordHash = passwordHash;
         CountryId = countryId;
         ProvinceId = provinceId;
      }
   }
}
