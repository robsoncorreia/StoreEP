using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreEP.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using StoreEP.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreEP.Controllers
{

    public class PedidoController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IPedidoRepositorio repository;
        private IEnderecoRepositorio _addressRepositoty;
        private Carrinho Carrinho;

        public PedidoController(IEnderecoRepositorio address,IPedidoRepositorio repoService, Carrinho cartService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            repository = repoService;
            Carrinho = cartService;
            _userManager = userManager;
            _signInManager = signInManager;
            _addressRepositoty = address;
        }

        public async Task<ViewResult> Lista()
        {
            ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                
            }
            ListaPedidoViewModels listaPedido = new ListaPedidoViewModels
            {
                Pedidos = repository.Pedidos.Where(o => o.UserID == user.Id),
                Enderecos = _addressRepositoty.Enderecos.ToList()
            };
            return View(listaPedido);
        }

        [HttpPost]
        public IActionResult MarkShipped(int ID)
        {
            Pedido order = repository.Pedidos.FirstOrDefault(o => o.ID == ID);
            if (order != null)
            {
                order.Shipped = true;
                repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Checkout(string enderecoid)
        {

            ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);
            Pedido pedido = new Pedido();
            var address = _addressRepositoty.Enderecos.SingleOrDefault(a => a.ID == int.Parse(enderecoid));
            pedido.Address = address;
            if (Carrinho.Lines?.Count() == 0)
            {
                ModelState.AddModelError("", "Descupe sua lista está vazia.");
                return RedirectToAction(actionName: "List", controllerName: "Produtos");
            }
            if (ModelState.IsValid)
            {
                pedido.UserID = user.Id;
                pedido.Lines = Carrinho.Lines.ToArray();
                repository.SaveOrder(pedido);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(pedido);
            }
        }
        public ViewResult Completed()
        {
            Carrinho.Limpar();
            return View();
        }
    }
}
