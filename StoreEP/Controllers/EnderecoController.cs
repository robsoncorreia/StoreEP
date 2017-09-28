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

    public class EnderecoController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEnderecoRepositorio _addressRepositoty;
        public EnderecoController(IEnderecoRepositorio addressRepositoty, UserManager<ApplicationUser> userManager)
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
            if (user != null && _addressRepositoty.Enderecos.Where(a => a.UserID.Equals(user.Id)).Count() != 0)
            {
                IEnumerable<Endereco> enderecos;
                enderecos = _addressRepositoty.Enderecos.Where(a => a.UserID.Equals(user.Id)).ToList();
                return View(enderecos);
            }
            return RedirectToAction(nameof(Criar));
        }
        [HttpPost]
        public async Task<IActionResult> Criar(Endereco address)
        {
            ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                address.UserID = user.Id;
            }
            if (ModelState.IsValid)
            {
                _addressRepositoty.SalvarEndereco(address);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Criar() => View();


        public IActionResult Apagar(string enderecoid)
        {
            Endereco apagarEndereco = _addressRepositoty.Enderecos.FirstOrDefault(p => p.ID == int.Parse(enderecoid));
            if (apagarEndereco != null)
            {
                _addressRepositoty.ApagarEndereco(int.Parse(enderecoid));
            }
            return RedirectToAction("Finalizar", "Pedido");
        }
    }
}
