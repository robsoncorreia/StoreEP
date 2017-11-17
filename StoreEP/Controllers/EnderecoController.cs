using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreEP.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using StoreEP.Models.ViewModels;

namespace StoreEP.Controllers
{

    public class EnderecoController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEnderecoRepositorio _EnderecoRepositorio;
        public EnderecoController(IEnderecoRepositorio addressRepositoty, UserManager<ApplicationUser> userManager)
        {
            _EnderecoRepositorio = addressRepositoty;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string returnUrl)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (user != null && _EnderecoRepositorio.Enderecos.Where(a => a.UserId.Equals(user.Id)).Count() != 0)
            {
                IEnumerable<Endereco> enderecos = _EnderecoRepositorio.Enderecos
                                                                        .Where(a => a.UserId.Equals(user.Id))
                                                                        .ToList();
                return View(enderecos);
            }
            TempData["endereco"] = "Você não possui endereços.";
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
                    _EnderecoRepositorio.SalvarEndereco(model);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        public IActionResult Criar()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Editar(string model )
        {
            Endereco editarEndereco = _EnderecoRepositorio.Enderecos
                                                            .FirstOrDefault(e => e.EnderecoID == int.Parse(model ?? "0"));
            ViewData["editar_endereco"] = true;
            return Json(editarEndereco);
        }

        public IActionResult Apagar(int enderecoId)
        {
            Endereco apagarEndereco = _EnderecoRepositorio.Enderecos.FirstOrDefault(e => e.EnderecoID == enderecoId);
            if (apagarEndereco != null)
            {
                _EnderecoRepositorio.ApagarEndereco(enderecoId);
                TempData["endereco"] = "Endereço apagado.";
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Utilizar(int enderecoId)
        {
            Endereco endereco = _EnderecoRepositorio.Enderecos.FirstOrDefault(e => e.EnderecoID == enderecoId);
            if (endereco != null)
            {
                endereco.DataUtilizacao = DateTime.Now;
                _EnderecoRepositorio.SalvarEndereco(endereco);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
