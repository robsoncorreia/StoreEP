using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreEP.Models;
using Microsoft.AspNetCore.Authorization;
using StoreEP.Models.ViewModels;
using StoreEP.Models.Interface;
using Microsoft.AspNetCore.Routing;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreEP.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IComentariosRepositorio _comentariosRepositorio;
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IImagensRepositorio _imagensRepositorio;

        public AdminController(
            IProdutoRepositorio produtoRepositorio,
            IComentariosRepositorio comentariosRepositorio,
            IPedidoRepositorio pedidoRepositorio,
            IImagensRepositorio imagensRepositorio)
        {
            _imagensRepositorio = imagensRepositorio;
            _pedidoRepositorio = pedidoRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _comentariosRepositorio = comentariosRepositorio;
        }

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

        public ViewResult Index() => View(/*new AdminIndexViewModel
        {
            ProdutosNãoEnviados = _pedidoRepositorio.Pedidos
                                                        .Where(p => p.Enviado == false)
                                                        .Count(),
            NumeroProdutosRegistrados = _produtoRepositorio.Produtos.Count(),
            ComentariosNaoAprovados = _comentariosRepositorio.Comentarios
                                                             .Where(c => c.Aprovado == false)
                                                             .Count()
        }*/);

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
        public IActionResult ValidarComentarios(int opcaoSelecionada, bool aprovado = false)
        {
            IEnumerable<Comentario> comentarios = _comentariosRepositorio.Comentarios
                                                                         .Where(c => c.Aprovado == aprovado);
            return View(comentarios);
        }

        public IActionResult AprovarComentario(int ID)
        {
            Comentario comentario = _comentariosRepositorio.Comentarios.SingleOrDefault(c => c.ProdutoID == ID);
            comentario.Aprovado = true;
            _comentariosRepositorio.RegistrarComentario(comentario);
            return RedirectToAction(nameof(ValidarComentarios));
        }

        [HttpGet("[controller]/[action]/{ID}")]
        public IActionResult DetalhesComentario(int ID)
        {
            Comentario comentario = _comentariosRepositorio.Comentarios.SingleOrDefault(c => c.ProdutoID == ID);
            return View(comentario);
        }

        [HttpGet("[controller]/[action]/{ID}")]
        public IActionResult RemoverComentario(int ID)
        {
            Comentario comentario = _comentariosRepositorio.Comentarios.SingleOrDefault(c => c.ProdutoID == ID);
            _comentariosRepositorio.ApagarComentario(comentario);
            TempData["massage"] = $"{comentario.NomeUsuario} apagado com sucesso.";
            return RedirectToAction(nameof(ValidarComentarios));
        }
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
    }
}
