using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreEP.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using StoreEP.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreEP.Controllers
{

    public class AddressController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAddressRepositoty _addressRepositoty;
        public AddressController(IAddressRepositoty addressRepositoty, UserManager<ApplicationUser> userManager)
        {
            _addressRepositoty = addressRepositoty;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);
            if (user != null && _addressRepositoty.Address.Where(a => a.UserID.Equals(user.Id)).Count() != 0)
            {
                return View(new AddressViewModel
                {
                    Address = _addressRepositoty.Address.Where(a => a.UserID.Equals(user.Id)).OrderBy(a => a.Line1).ToList()
                });
            }
            return RedirectToAction("Create", "Adrress");
        }
        public IActionResult Create() => View();

        public IActionResult Create(Address address)
        {
            return View();
        }
    }
}
