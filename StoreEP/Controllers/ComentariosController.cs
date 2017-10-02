using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using StoreEP.Models.Interface;
using StoreEP.Models;

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
                comentario.UsuarioID = user.Id;
                comentario.Data = DateTime.Now;
                _comentariosRepositorio.RegistrarComentario(comentario);
            }
            return RedirectToAction(actionName:"Finalizar", controllerName:"Pedido");
        }
    }
}
