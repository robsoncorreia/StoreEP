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
using StoreEP.Models.Interface;

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
        private IImagensRepositorio _imagemRepositorio;
        private Carrinho _carrinho;

        private StoreEPDbContext _bancoContexto;

        public PedidoController(
            IImagensRepositorio imagemRepositorio,
            IProdutoRepositorio produtoRepositorio,
            IEnderecoRepositorio address,
            IPedidoRepositorio repoService,
            Carrinho cartService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            StoreEPDbContext bancoContexto)
        {
            _pedidoRepositorio = repoService;
            _carrinho = cartService;
            _userManager = userManager;
            _signInManager = signInManager;
            _enderecoRepositorio = address;
            _produtoRepositorio = produtoRepositorio;
            _imagemRepositorio = imagemRepositorio;
            _bancoContexto = bancoContexto;
        }

        public async Task<ViewResult> Lista()
        {
            ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);

            //Task.Run(() => new List<string>() { "a", "b" });
            //IEnumerable<Pedido> pedidos = await Task.Run(() =>  _bancoContexto.Pedidos.Where(o => o.UserId.Equals(user.Id)).ToList());
            IEnumerable<Pedido> pedidos = _pedidoRepositorio.Pedidos.Where(o => o.UserId.Equals(user.Id)).ToList();
            IEnumerable<Endereco> enderecos = _enderecoRepositorio.Enderecos.Where(e => e.UserId.Equals(user.Id)).ToList();


            return View(new PedidoListaViewModel
            {
                Pedidos = pedidos,
                Enderecos = enderecos
            });
        }

        [HttpPost]
        public IActionResult MarkShipped(int ID)
        {
            Pedido pedido = _pedidoRepositorio.Pedidos.FirstOrDefault(p => p.ID == ID);
            if (pedido != null)
            {
                pedido.Shipped = true;
                _pedidoRepositorio.Registrar(pedido);
            }
            return RedirectToAction(nameof(Lista));
        }

        [HttpPost]
        public async Task<IActionResult> Finalizar(FinalizarPedidoViewModel model)
        {
            ClaimsPrincipal currentUser = this.User;
            Pedido pedido = new Pedido();

            var user = await _userManager.GetUserAsync(User);
            Endereco endereco = _enderecoRepositorio.Enderecos
                                                    .SingleOrDefault(e => e.ID == model.EnderecoId);

            pedido.Endereco = endereco;
            pedido.DataCompra = DateTime.Now;
            pedido.Pagamento = model.Pagamento;

            model.Pagamento.UserId = user.Id;
            model.Pagamento.CompraDT = DateTime.Now;

            if (_carrinho.Lines?.Count() == 0)
            {
                ModelState.AddModelError("", "Descupe sua lista está vazia.");
                return RedirectToAction(actionName: "List", controllerName: "Produtos");
            }
            if (ModelState.IsValid)
            {
                pedido.UserId = user.Id;
                pedido.Lines = _carrinho.Lines.ToArray();
                _pedidoRepositorio.Registrar(pedido);
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
            if (_enderecoRepositorio.Enderecos.Where(e => e.UserId.Equals(user.Id) && !e.UserId.Equals("apagado")).Count() == 0)
            {
                return RedirectToAction(actionName: "Criar", controllerName: "Endereco");
            }
            return View(new FinalizarPedidoViewModel
            {
                Carrinho = _carrinho,
                Enderecos = _enderecoRepositorio.Enderecos.Where(a => a.UserId.Equals(user.Id)).ToList(),
                Imagens = _imagemRepositorio.Imagens.ToList()
            });
        }
        public RedirectToActionResult RemoverCarrinho(int ID)
        {
            Produto produto = _produtoRepositorio.Produtos.FirstOrDefault(p => p.ProdutoID == ID);
            if (produto != null)
            {
                _carrinho.RemoveLine(produto);
            }
            return RedirectToAction("Finalizar", "Pedido");
        }
    }
}

