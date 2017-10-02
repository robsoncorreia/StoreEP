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
            return View(new PedidoListaViewModel {
                Pedidos = _pedidoRepositorio.Pedidos.Where(o => o.UserId == user.Id)
            });
        }

        [HttpPost]
        public IActionResult MarkShipped(int ID)
        {
            Pedido pedido = _pedidoRepositorio.Pedidos.FirstOrDefault(p => p.PedidoId == ID);
            if (pedido != null)
            {
                pedido.Shipped = true;
                _pedidoRepositorio.SaveOrder(pedido);
            }
            return RedirectToAction(nameof(Lista));
        }

        [HttpPost]
        public async Task<IActionResult> Finalizar(FinalizarPedidoViewModel finalizarPedidoViewModel)
        {
            ClaimsPrincipal currentUser = this.User;
            Pedido pedido = new Pedido();

            var user = await _userManager.GetUserAsync(User);
            Endereco endereco = _enderecoRepositorio.Enderecos.SingleOrDefault(e => e.EnderecoId == int.Parse("1"));

            pedido.Endereco = endereco;
            pedido.DataCompra = DateTime.Now;
            pedido.Pagamento = finalizarPedidoViewModel.Pagamento;

            finalizarPedidoViewModel.Pagamento.UserId = user.Id;
            finalizarPedidoViewModel.Pagamento.CompraDT = DateTime.Now ;
            
            if (_carrinho.Lines?.Count() == 0)
            {
                ModelState.AddModelError("", "Descupe sua lista está vazia.");
                return RedirectToAction(actionName: "List", controllerName: "Produtos");
            }
            if (ModelState.IsValid)
            {
                pedido.UserId = user.Id;
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
        public async Task<IActionResult> Finalizar()
        {
            ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                if (_enderecoRepositorio.Enderecos.Where(e => e.UserId.Equals(user.Id) && !e.UserId.Equals("apagado")).Count() == 0)
                {
                    return RedirectToAction(actionName: "Criar", controllerName: "Endereco");
                }
            }
            return View(new FinalizarPedidoViewModel
            {
                Carrinho = _carrinho,
                Enderecos = _enderecoRepositorio.Enderecos.Where(a => a.UserId.Equals(user.Id)).ToList()
            });
        }
        public RedirectToActionResult RemoverCarrinho(int ID)
        {
            Produto produto = _produtoRepositorio.Produtos.FirstOrDefault(p => p.ProdutoId == ID);
            if (produto != null)
            {
                _carrinho.RemoveLine(produto);
            }
            return RedirectToAction("Finalizar", "Pedido");
        }
    }
}

