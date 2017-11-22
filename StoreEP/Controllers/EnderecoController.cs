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
                ViewLogin();
            }
            IEnumerable<Endereco> enderecos = _EnderecoRepositorio.Enderecos
                                                                       .Where(a => a.UserId.Equals(user.Id))
                                                                       .ToList();
            if (enderecos.Count() == 0)
            {
                Alerta("Você não possui endereços.");
            }
            return View(enderecos);
        }
        [HttpPost]
        public async Task<JsonResult> Criar(Endereco model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ViewLogin();
            }
            model.UserId = user.Id;
            ModelState.Remove("UserId");
            if (!ModelState.IsValid)
            {
                return Json(null);
            }
            if (_EnderecoRepositorio.SalvarEndereco(model) == 0)
            {
                return Json(null);
            }
            Alerta("Endereço criado.");
            return Json(GetUltimoEnderecoUtilizado(user));
        }
        [HttpPost]
        public async Task<JsonResult> Editar(Endereco model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ViewLogin();
            }
            model.UserId = user.Id;
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                _EnderecoRepositorio.SalvarEndereco(model);
                Alerta("Endereço editado.");
                return Json(model);
            }
            return Json(model);
        }
        [HttpPost]
        public JsonResult GetEndereco(int model)
        {
            Endereco editarEndereco = _EnderecoRepositorio.Enderecos
                                                            .FirstOrDefault(e => e.EnderecoID == model);
            Alerta("true");
            return Json(editarEndereco);
        }

        public IActionResult Apagar(int enderecoId)
        {
            Endereco apagarEndereco = _EnderecoRepositorio.Enderecos.FirstOrDefault(e => e.EnderecoID == enderecoId);
            if (apagarEndereco != null)
            {
                _EnderecoRepositorio.ApagarEndereco(enderecoId);
                Alerta("Endereço apagado.");
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
        private Endereco GetUltimoEnderecoUtilizado(IdentityUser user)
        {
            return _EnderecoRepositorio.Enderecos.FirstOrDefault(a => a.UserId.Equals(user.Id) && a.DataUtilizacao == (_EnderecoRepositorio.Enderecos.Where(e => e.UserId.Equals(user.Id)).Select(e => e.DataUtilizacao).Max()));
        }
        private ActionResult ViewLogin()
        {
            return RedirectToAction(actionName: "Login", controllerName: "Account");
        }
        private void Alerta(string mensagem)
        {
            TempData["endereco"] = mensagem;
        }
    }
}
