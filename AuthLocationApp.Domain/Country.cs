using AuthLocationApp.Domain.Exceptions;

namespace AuthLocationApp.Domain
{
   public class Country
   {
      private readonly List<Province> _provinces = new();

      public int Id { get; private set; }
      public string Name { get; private set; }
      public IReadOnlyCollection<Province> Provinces => _provinces.AsReadOnly();

      public Country(int id, string name)
      {
         if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Country name cannot be empty.");

         Id = id;
         Name = name;
      }

      public void AddProvince(Province province)
      {
         if (province == null)
            throw new DomainException("Province cannot be null.");

         _provinces.Add(province);
      }

      public void RemoveProvince(int provinceId)
      {
         var province = _provinces.FirstOrDefault(p => p.Id == provinceId);

         if (province is null)
            throw new DomainException($"Province with ID: '{provinceId}' does not exist in '{Name}'");

         _provinces.Remove(province);
      }
   }
}
