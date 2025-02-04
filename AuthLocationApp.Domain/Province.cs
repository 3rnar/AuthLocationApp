using AuthLocationApp.Domain.Exceptions;

namespace AuthLocationApp.Domain
{
   public class Province
   {
      public int Id { get; private set; }
      public string Name { get; private set; }
      public int CountryId { get; private set; }

      public Province(int id, string name, int countryId)
      {
         if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Province name cannot be empty.");

         Id = id;
         Name = name;
         CountryId = countryId;
      }

      public void ChangeName(string name)
      {
         if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Province name cannot be empty.");

         Name = name;
      }
   }
}
