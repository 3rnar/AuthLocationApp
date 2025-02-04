using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLocationApp.Application.Interfaces.Services
{
   public interface IPasswordHasher
   {
      string HashPassword(string plainPassword);
      bool VerifyPassword(string plainPassword, string hashedPassword);
   }
}
