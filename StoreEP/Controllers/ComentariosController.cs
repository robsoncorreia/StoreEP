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
    public class ComentariosController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IComentariosRepositorio _comentariosRepositorio;
        public ComentariosController(
            UserManager<ApplicationUser> userManager,
            IComentariosRepositorio comentariosRepositorio)
        {
            _userManager = userManager;
            _comentariosRepositorio = comentariosRepositorio;
        }
        public async Task<IActionResult> Comentar(Comentario comentario)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                comentario.NomeUsuario = user.Nome;
                comentario.UsuarioID = user.Id;
                comentario.Data = DateTime.Now;
                _comentariosRepositorio.RegistrarComentario(comentario);
            }
            return RedirectToAction(actionName: "Finalizar", controllerName: "Pedido");
        }
        public IActionResult Responder(DetalheProdutoViewModels detalheProdutoViewModels, int ID)
        {
            Comentario comentario = _comentariosRepositorio.Comentarios.SingleOrDefault(c => c.ProdutoID == ID);
            if (comentario == null)
            {
                return RedirectToAction(actionName: "Finalizar", controllerName: "Pedido");
            }
            comentario.Respostas = new List<Comentario> {
                detalheProdutoViewModels.Resposta
            };
            _comentariosRepositorio.RegistrarComentario(comentario);
            return RedirectToAction(actionName: "Finalizar", controllerName: "Pedido");
        }
    }
}
