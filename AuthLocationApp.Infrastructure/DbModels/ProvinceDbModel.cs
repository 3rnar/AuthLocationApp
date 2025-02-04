using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLocationApp.Infrastructure.DbModels
{
   public class ProvinceDbModel : BaseDbModel
   {
      public string Name { get; set; } = null!;
      public int CountryId { get; set; }
      public CountryDbModel Country { get; set; } = null!;

      public ICollection<UserDbModel> Users { get; set; } = new List<UserDbModel>();
   }
}
