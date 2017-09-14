using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser(string userName) : base(userName)
        {
        }
    }
}
