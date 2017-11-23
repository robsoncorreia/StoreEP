using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreEP.Models;
using Microsoft.AspNetCore.Authorization;
using StoreEP.Models.ViewModels;
using StoreEP.Models.Interface;

namespace StoreEP.Controllers
{
    //[Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IAvaliacaoRepositorio _avaliacoesRepositorio;
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IImagensRepositorio _imagensRepositorio;

        public AdminController(
            IProdutoRepositorio produtoRepositorio,
            IAvaliacaoRepositorio comentariosRepositorio,
            IPedidoRepositorio pedidoRepositorio,
            IImagensRepositorio imagensRepositorio)
        {
            _imagensRepositorio = imagensRepositorio;
            _pedidoRepositorio = pedidoRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _avaliacoesRepositorio = comentariosRepositorio;
        }

        public ViewResult Index() => View();

        #region Pedido
        [HttpGet]
        public IActionResult PedidosNaoEnviados(int page = 1)
        {
            int itensPorPagina = 1;
            IEnumerable<Pedido> pedidos = _pedidoRepositorio.Pedidos.Where(p => p.Enviado == false).ToList();
            int total = pedidos.Count();
            pedidos = pedidos.Skip((page - 1) * itensPorPagina)
                                .Take(itensPorPagina)
                                .ToList();

            NaoEnvidoViewModel model = new NaoEnvidoViewModel
            {
                Pedidos = pedidos,
                PagingInfo = new PagingInfo
                {
                    TotalItems = total,
                    ItensPerPage = itensPorPagina,
                    CurrentPage = page
                }
            };
            return View(model);
        }
        #endregion
        #region Produto
        [HttpGet("[controller]/[action]/{produtoID}")]
        public ActionResult ConfirmaExclusaoProduto(int produtoID)
        {
            Produto produto = _produtoRepositorio.Produtos.SingleOrDefault(p => p.ProdutoID == produtoID);
            if (produto != null)
            {
                return View(produto);
            }
            return RedirectToAction(nameof(ListarTodosProdutos));
        }

        [HttpGet]
        public ViewResult ListarTodosProdutos(int opcaoSelecionada, int page = 1)
        {
            int itensPorPagina = 5;
            IEnumerable<Produto> produtos = _produtoRepositorio.Produtos
                                                                  .OrderBy(p => p.ProdutoID)
                                                                  .Skip((page - 1) * itensPorPagina)
                                                                  .Take(itensPorPagina).ToList();
            ProductsListViewModel model = new ProductsListViewModel
            {
                Produtos = produtos,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItensPerPage = 5,
                    TotalItems = _produtoRepositorio.Produtos.Count()
                }
            };
            return View(model);
        }

        [AutoValidateAntiforgeryToken]
        [HttpGet("[controller]/[action]/{produtoID}")]
        public ViewResult EditarProduto(int produtoID)
        {
            Produto produto = _produtoRepositorio.Produtos.FirstOrDefault(p => p.ProdutoID == produtoID);
            return View(new EditarProdutoViewModel
            {
                Produto = produto,
                Fabricantes = GetFabricantes(),
                Categorias = GetCategorias()
            });
        }

        [HttpPost]
        public IActionResult EditarProduto(EditarProdutoViewModel model)
        {
            int ID = model.Produto.ProdutoID;
            if (ModelState.IsValid)
            {
                if (model.Imagem != null)
                {
                    _imagensRepositorio.RegistrarImagem(model.Imagem);
                }
                TempData["massage"] = $"{model.Produto.Nome} foi salvo com sucesso.";
                model.Fabricantes = GetFabricantes();
                model.Categorias = GetCategorias();
                return RedirectToAction("EditarProduto", ID);
            }
            else
            {
                ModelState.Remove("Imagem.ID");
                ModelState.Remove("Imagem.Link");
                ModelState.Remove("Imagem.Nome");
                if (ModelState.IsValid)
                {
                    TempData["massage"] = $"{model.Produto.Nome} foi salvo com sucesso.";
                    _produtoRepositorio.RegistrarProduto(model.Produto);
                }
                model.Fabricantes = GetFabricantes();
                model.Categorias = GetCategorias();
                return RedirectToAction("EditarProduto", ID);
            }
        }
        public LocalRedirectResult ApagarImagem(int id, string url = null)
        {
            _imagensRepositorio.ApagarImagem(id);
            return LocalRedirect(url);
        }
        public ActionResult CriarProduto()
        {
            return View(new EditarProdutoViewModel
            {
                Categorias = GetCategorias(),
                Fabricantes = GetFabricantes()
            });
        }

        [HttpPost]
        public ActionResult CriarProduto(EditarProdutoViewModel editarProdutoViewModel)
        {
            editarProdutoViewModel.Categorias = GetCategorias();
            editarProdutoViewModel.Fabricantes = GetFabricantes();
            ModelState.Remove("Produto.ProdutoID");
            ModelState.Remove("Imagem.ImagemID");
            if (ModelState.IsValid)
            {
                editarProdutoViewModel.Produto.Imagens = new List<Imagem> { editarProdutoViewModel.Imagem };
                _produtoRepositorio.RegistrarProduto(editarProdutoViewModel.Produto);
                TempData["massage"] = $"{editarProdutoViewModel.Produto.Nome} foi salvo com sucesso.";
                return RedirectToAction(nameof(ListarTodosProdutos));
            }
            return View(editarProdutoViewModel);
        }

        [HttpGet("[controller]/[action]/{produtoID}")]
        public IActionResult ApagarProduto(int produtoID)
        {
            Produto deletedProduto = _produtoRepositorio.ApagarProduto(produtoID);
            if (deletedProduto != null)
            {
                TempData["message"] = $"{deletedProduto.Nome} foi apagado.";
            }
            return RedirectToAction(nameof(ListarTodosProdutos));
        }
        #endregion
        #region Avaliacao

        public IActionResult ValidarAvaliacoes(int opcaoSelecionada, bool aprovado = false)
        {
            IEnumerable<Avaliacao> avaliacoes = _avaliacoesRepositorio.Avaliacoes
                                                                         .Where(c => c.Aprovado == aprovado);
            return View(avaliacoes);
        }

        public IActionResult AprovarAvaliacao(int avaliacaoID)
        {
            Avaliacao avaliacao = _avaliacoesRepositorio.Avaliacoes
                                                        .SingleOrDefault(c => c.AvaliacaoID == avaliacaoID);
            if (avaliacao != null)
            {
                avaliacao.Aprovado = true;
                _avaliacoesRepositorio.RegistrarComentario(avaliacao);
            }

            return RedirectToAction(nameof(ValidarAvaliacoes));
        }

        [HttpGet("[controller]/[action]/{ID}")]
        public IActionResult DetalhesAvaliacao(int ID)
        {
            Avaliacao avaliacao = _avaliacoesRepositorio.Avaliacoes.SingleOrDefault(c => c.ProdutoID == ID);
            return View(avaliacao);
        }

        [HttpGet("[controller]/[action]/{avaliacaoID}")]
        public IActionResult RemoverAvaliacao(int avaliacaoID)
        {
            Avaliacao avaliacao = _avaliacoesRepositorio.Avaliacoes
                                                        .SingleOrDefault(a => a.AvaliacaoID == avaliacaoID);
            if (avaliacao != null)
            {
                _avaliacoesRepositorio.ApagarComentario(avaliacao);
                TempData["massage"] = $"{avaliacao.NomeUsuario} apagado com sucesso.";
            }

            return RedirectToAction(nameof(ValidarAvaliacoes));
        }
        #endregion
        #region Utilidade
        public IEnumerable<string> GetCategorias()
        {
            return _produtoRepositorio.Produtos
                                        .Where(c => c.Categoria != null)
                                        .OrderBy(c => c.Categoria)
                                        .Select(c => c.Categoria)
                                        .Distinct()
                                        .ToList();
        }
        public IEnumerable<string> GetFabricantes()
        {
            return _produtoRepositorio.Produtos
                                        .Where(c => c.Fabricante != null)
                                        .OrderBy(c => c.Fabricante)
                                        .Select(c => c.Fabricante)
                                        .Distinct()
                                        .ToList();
        }
        #endregion
    }
}
