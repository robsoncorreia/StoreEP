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
            _bancoContexto = bancoContexto;
        }

        public async Task<ViewResult> ListarPedidos()
        {
            var user = await _userManager.GetUserAsync(User);
            IEnumerable<Pedido> pedidos = _pedidoRepositorio.Pedidos.Where(o => o.UserId.Equals(user.Id)).ToList();

            return View(pedidos);
        }

        [HttpGet("[controller]/[action]/{produtoID}")]
        public IActionResult MarcarEnviado(int produtoID)
        {
            Pedido pedido = _pedidoRepositorio.Pedidos.FirstOrDefault(p => p.PedidoID == produtoID);
            if (pedido != null)
            {
                pedido.Enviado = true;
                _pedidoRepositorio.Registrar(pedido);
            }
            return RedirectToAction(controllerName:"Admin", actionName: "PedidosNaoEnviados");
        }

        [HttpPost]
        public async Task<IActionResult> Finalizar(FinalizarPedidoViewModel model)
        {
            Pedido pedido = new Pedido();
            var user = await _userManager.GetUserAsync(User);
            Endereco endereco = _enderecoRepositorio.Enderecos
                                                    .SingleOrDefault(e => e.EnderecoID == model.EnderecoId);
            pedido.Endereco = endereco;
            pedido.Pagamento = model.Pagamento;
            model.Pagamento.UserId = user.Id;

            if (_carrinho.QuantidadeTotal() == 0)
            {
                TempData["carrinho"] = "Você não escolheu nenhum produto.";
                return RedirectToAction(actionName: "Listar", controllerName: "Produtos");
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
            var user = await _userManager.GetUserAsync(User);
            var enderecos = _enderecoRepositorio.Enderecos
                                                .Where(e => e.UserId.Equals(user.Id))
                                                .ToList();
            if (_carrinho.QuantidadeTotal() == 0)
            {
                TempData["carrinho"] = "Seu carrinho de compras esta vazio.";
                return RedirectToAction(actionName: "Listar", controllerName: "Produtos");
            }

            bool naoEndereco = enderecos.Where(e => !e.UserId.Equals("apagado")).Count() == 0;

            if (naoEndereco)
            {
                TempData["endereco"] = "Você não possui endereços cadastrados.";
                return RedirectToAction(actionName: "Index", controllerName: "Endereco");
            }
                      
            return View(new FinalizarPedidoViewModel
            {
                Carrinho = _carrinho,
                Endereco = enderecos.Where(e => e.DataUtilizacao ==
                                          (enderecos.Select(d => d.DataUtilizacao).Max()))
                                            .SingleOrDefault()
            });
        }
        public RedirectToActionResult RemoverCarrinho(int produtoID)
        {
            Produto produto = _produtoRepositorio.Produtos
                                                    .FirstOrDefault(p => p.ProdutoID == produtoID);
            if (produto != null)
            {
                _carrinho.RemoveLine(produto);
            }
            return RedirectToAction("Finalizar", "Pedido");
        }
    }
}

