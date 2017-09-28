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

namespace StoreEP.Controllers
{
    [Authorize]
    public class PedidoController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IPedidoRepositorio _pedidoRepositorio;
        private IEnderecoRepositorio _enderecoRepositorio;
        private IProdutoRepositorio _produtoRepositorio;
        private Carrinho _carrinho;

        public PedidoController(
            IProdutoRepositorio produtoRepositorio,
            IEnderecoRepositorio address,
            IPedidoRepositorio repoService,
            Carrinho cartService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _pedidoRepositorio = repoService;
            _carrinho = cartService;
            _userManager = userManager;
            _signInManager = signInManager;
            _enderecoRepositorio = address;
            _produtoRepositorio = produtoRepositorio;
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
                Pedidos = _pedidoRepositorio.Pedidos.Where(o => o.UserID == user.Id),
                Enderecos = _enderecoRepositorio.Enderecos.ToList()
            };
            return View(listaPedido);
        }

        [HttpPost]
        public IActionResult MarkShipped(int ID)
        {
            Pedido order = _pedidoRepositorio.Pedidos.FirstOrDefault(o => o.ID == ID);
            if (order != null)
            {
                order.Shipped = true;
                _pedidoRepositorio.SaveOrder(order);
            }
            return RedirectToAction(nameof(Lista));
        }

        [HttpPost]
        public async Task<IActionResult> Finalizar (FinalizarPedidoViewModel finalizarPedidoViewModel)
        {
            ClaimsPrincipal currentUser = this.User;
            Pedido pedido = new Pedido();
            var user = await _userManager.GetUserAsync(User);
            var address = _enderecoRepositorio.Enderecos.SingleOrDefault(a => a.ID == int.Parse("1"));
            pedido.Address = address;
            if (_carrinho.Lines?.Count() == 0)
            {
                ModelState.AddModelError("", "Descupe sua lista está vazia.");
                return RedirectToAction(actionName: "List", controllerName: "Produtos");
            }
            if (ModelState.IsValid)
            {
                pedido.UserID = user.Id;
                pedido.Lines = _carrinho.Lines.ToArray();
                _pedidoRepositorio.SaveOrder(pedido);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(pedido);
            }
        }
        public ViewResult Completed()
        {
            _carrinho.Limpar();
            return View();
        }
        public async Task<ViewResult> Finalizar()
        {
            ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View(nameof(Finalizar));
            }
            return View(new FinalizarPedidoViewModel
            {
                Carrinho = _carrinho,
                Enderecos = _enderecoRepositorio.Enderecos.Where(a => a.UserID.Equals(user.Id)).ToList()
            });
        }
        public RedirectToActionResult RemoverCarrinho(int produtoID)
        {
            Produto produto = _produtoRepositorio.Produtos.FirstOrDefault(p => p.ProdutoID == produtoID);
            if (produto != null)
            {
                _carrinho.RemoveLine(produto);
            }
            return RedirectToAction("Finalizar", "Pedido");
        }
    }
}

