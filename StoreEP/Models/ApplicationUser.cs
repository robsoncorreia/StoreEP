using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace StoreEP.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Apelido { get; set; }
        [Required]
        public string Nome { get; set; }
        public string NomeSocial { get; set; }
        public string SobreNome { get; set; }
    }
}
