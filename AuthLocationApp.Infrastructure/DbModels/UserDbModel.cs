using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLocationApp.Infrastructure.DbModels
{
   public class UserDbModel : BaseDbModel
   {
      public string Email { get; set; } = null!;
      public string PasswordHash { get; set; } = null!;
      public int CountryId { get; set; }
      public int ProvinceId { get; set; }

      // Навигационные свойства
      public CountryDbModel Country { get; set; } = null!;
      public ProvinceDbModel Province { get; set; } = null!;
   }
}
