using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLocationApp.Application.DTOs.Users
{
   public record UserCreateDto(
       string Email,
       string Password,
       int? CountryId,
       int? ProvinceId
   );
}
