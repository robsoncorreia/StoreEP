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
            if (user != null && _addressRepositoty.Enderecos.Where(a => a.UserId.Equals(user.Id)).Count() != 0)
            {
                IEnumerable<Endereco> enderecos;
                enderecos = _addressRepositoty.Enderecos.Where(a => a.UserId.Equals(user.Id)).ToList();
                return View(enderecos);
            }
            return RedirectToAction(nameof(Criar));
        }
        [HttpPost]
        public async Task<IActionResult> Criar(Endereco model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                model.UserId = user.Id;
                ModelState.Remove("UserId");
                if (ModelState.IsValid)
                {
                    _addressRepositoty.SalvarEndereco(model);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        public IActionResult Criar() => View();


        public IActionResult Apagar(string ID)
        {
            Endereco apagarEndereco = _addressRepositoty.Enderecos.FirstOrDefault(e => e.EnderecoID == int.Parse(ID));
            if (apagarEndereco != null)
            {
                _addressRepositoty.ApagarEndereco(int.Parse(ID));
            }
            return RedirectToAction("Finalizar", "Pedido");
        }
        public IActionResult Utilizar(int ID)
        {
            Endereco endereco = _addressRepositoty.Enderecos.FirstOrDefault(e => e.EnderecoID == ID);
            if (endereco != null)
            {
                endereco.DataUtilizacao = DateTime.Now;
                _addressRepositoty.SalvarEndereco(endereco);
            }
            return RedirectToAction("Finalizar", "Pedido");
        }
    }
}
