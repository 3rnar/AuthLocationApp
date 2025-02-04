using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLocationApp.Infrastructure.DbModels
{
   public class CountryDbModel : BaseDbModel
   {
      public string Name { get; set; } = null!;

      public ICollection<ProvinceDbModel> Provinces { get; set; } = new List<ProvinceDbModel>();

      public ICollection<UserDbModel> Users { get; set; } = new List<UserDbModel>();
   }
}
