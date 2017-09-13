using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StoreEP.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser(string userName) : base(userName)
        {
        }
    }
}
