using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLocationApp.Application.DTOs.Users
{
   public record UserDto(
       int Id,
       string Email,
       int CountryId,
       int ProvinceId
   );
}
