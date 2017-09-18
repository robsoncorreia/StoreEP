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
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (user != null && _addressRepositoty.Address.Where(a => a.UserID.Equals(user.Id)).Count() != 0)
            {
                return View(new AddressViewModel
                {
                    GetAddress = _addressRepositoty.Address.Where(a => a.UserID.Equals(user.Id)).ToList()
                });
            }
            return RedirectToAction(nameof(Create));
        }
        [HttpPost]
        public async Task<IActionResult> Create(Address address)
        {
            ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                address.UserID = user.Id;
            }
            if (ModelState.IsValid)
            {
                _addressRepositoty.SaveAddress(address);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Create() => View();
    }
}
