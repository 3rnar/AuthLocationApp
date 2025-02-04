using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLocationApp.Infrastructure.Mappers
{
   public interface IMapper<TSource, TDestination>
   {
      TDestination ToDomain(TSource source);
      TSource ToDbModel(TDestination domain);
   }
}
