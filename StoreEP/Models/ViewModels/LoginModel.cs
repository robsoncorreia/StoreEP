using System.ComponentModel.DataAnnotations;

namespace StoreEP.Models.ViewModels
{
    public class LoginModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [UIHint(uiHint: "password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
