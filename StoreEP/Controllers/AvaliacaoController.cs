using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using StoreEP.Models.Interface;
using StoreEP.Models;
using StoreEP.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreEP.Controllers
{
    public class AvaliacaoController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IAvaliacaoRepositorio _avaliacoesRepositorio;
        private IProdutoRepositorio _produtoRepositorio;
        public AvaliacaoController(
            UserManager<ApplicationUser> userManager,
            IAvaliacaoRepositorio comentariosRepositorio,
            IProdutoRepositorio produtoRepositorio)
        {
            _userManager = userManager;
            _avaliacoesRepositorio = comentariosRepositorio;
            _produtoRepositorio = produtoRepositorio;
        }
        public async Task<IActionResult> Avaliar(Avaliacao avaliacao, string url = null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                avaliacao.NomeUsuario = user.Nome;
                avaliacao.UsuarioID = user.Id;
                _avaliacoesRepositorio.RegistrarComentario(avaliacao);
                if (url != null)
                {
                    TempData["mensagem"] = $"{user.Nome} sua avaliação será submetida a aprovação.";
                    return LocalRedirect(url);
                }
            }
            return RedirectToAction(actionName: "Finalizar", controllerName: "Pedido");
        }
        public async Task<IActionResult> Responder(DetalheProdutoViewModels model)
        {
            Avaliacao avaliacao = _avaliacoesRepositorio.Avaliacoes
                                                        .SingleOrDefault(a => a.AvaliacaoID == model.Avaliacao.AvaliacaoID);
            if (avaliacao != null && model.Resposta != null)
            {
                var user = await _userManager.GetUserAsync(User);
                model.Resposta.UsuarioID = user.Id;
                model.Resposta.NomeUsuario = user.Nome;
                _avaliacoesRepositorio.RegistrarResposta(avaliacao, model.Resposta);
            }
            return RedirectToAction(actionName: "Finalizar", controllerName: "Pedido");
        }
    }
}
