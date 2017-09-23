using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreEP.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreEP.Controllers
{

    public class PedidoController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IOrderRepository repository;
        private IAddressRepositoty _addressRepositoty;
        private Carrinho Carrinho;

        public PedidoController(IAddressRepositoty address,IOrderRepository repoService, Carrinho cartService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            repository = repoService;
            Carrinho = cartService;
            _userManager = userManager;
            _signInManager = signInManager;
            _addressRepositoty = address;
        }

        public ViewResult List() => View(repository.Orders.Where(o => !o.Shipped));

        [HttpPost]
        public IActionResult MarkShipped(int ID)
        {
            Pedido order = repository.Orders.FirstOrDefault(o => o.ID == ID);
            if (order != null)
            {
                order.Shipped = true;
                repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }
        //[HttpGet]
        //public async Task<IActionResult> CheckoutVal()
        //{

        //    ClaimsPrincipal currentUser = this.User;
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //    return Redirect(Url.Action("Checkout", "Order", new Order()));
        //    //return RedirectToAction(actionName: "Checkout", controllerName: "Order", new Order());
        //}

        [HttpPost]
        public async Task<IActionResult> Checkout(int ID)
        {
            ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Pedido order = new Pedido();
            var address = _addressRepositoty.Address.SingleOrDefault(a => a.ID == ID);
            order.Address = address;
            if (Carrinho.Lines?.Count() == 0)
            {
                ModelState.AddModelError("", "Descupe sua lista está vazia.");
                return RedirectToAction(actionName: "List", controllerName: "Produtos");
            }
            if (ModelState.IsValid)
            {
                order.Lines = Carrinho.Lines.ToArray();
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }
        public ViewResult Completed()
        {
            Carrinho.Clear();
            return View();
        }
    }
}
