using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Digite seu email.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite uma senha válida.")]
        [DataType(DataType.Password, ErrorMessage ="Digite uma senha válida.")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
