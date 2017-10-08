using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreEP.Models;
using Microsoft.AspNetCore.Authorization;
using StoreEP.Models.ViewModels;
using StoreEP.Models.Interface;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreEP.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        private IProdutoRepositorio _produtoRepositorio;
        private IImagensRepositorio _imagensRepositorio;
        private IComentariosRepositorio _comentariosRepositorio;

        public AdminController(IProdutoRepositorio repo, IImagensRepositorio imagens, IComentariosRepositorio comentariosRepositorio)
        {
            _produtoRepositorio = repo;
            _imagensRepositorio = imagens;
            _comentariosRepositorio = comentariosRepositorio;
        }
        public ViewResult Index() => View();
        public ViewResult Listar() => View(_produtoRepositorio.Produtos);

        [AutoValidateAntiforgeryToken]
        [HttpGet("[controller]/[action]/Produto{batata}")]
        public ViewResult Editar(int batata)
        {
            return View(new EditarProdutoViewModel
            {
                Imagens = _imagensRepositorio.Imagens.Where(i => i.ProdutoId == batata).ToList(),
                Fabricantes = _produtoRepositorio.Produtos.Select(f => f.Fabricante).OrderBy(f => f).ToList(),
                Produto = _produtoRepositorio.Produtos.FirstOrDefault(p => p.ProdutoId == batata),
                Categorias = _produtoRepositorio.Produtos.Select(x => x.Categoria).Distinct().OrderBy(x => x)
            });
        }
        [HttpPost]
        public IActionResult Editar(EditarProdutoViewModel editarProdutoViewModel)
        {
            if (ModelState.IsValid)
            {
                if (editarProdutoViewModel.Imagem != null)
                {
                    _imagensRepositorio.RegistrarImagem(editarProdutoViewModel.Imagem);
                }
                TempData["massage"] = $"{editarProdutoViewModel.Produto.Nome} foi salvo com sucesso.";              
                editarProdutoViewModel.Imagens = _imagensRepositorio.Imagens.Where(i => i.ProdutoId == editarProdutoViewModel.Produto.ProdutoId).ToList();
                editarProdutoViewModel.Fabricantes = _produtoRepositorio.Produtos.Select(f => f.Fabricante).OrderBy(f => f).ToList();
                editarProdutoViewModel.Categorias = _produtoRepositorio.Produtos.Select(x => x.Categoria).Distinct().OrderBy(x => x);
                return View(editarProdutoViewModel);
            }
            else
            {
                ModelState.Remove("Imagem.Link");
                ModelState.Remove("Imagem.Nome");
                if (ModelState.IsValid)
                {
                    TempData["massage"] = $"{editarProdutoViewModel.Produto.Nome} foi salvo com sucesso.";
                    _produtoRepositorio.RegistrarProduto(editarProdutoViewModel.Produto);
                }
                editarProdutoViewModel.Imagens = _imagensRepositorio.Imagens.Where(i => i.ProdutoId == editarProdutoViewModel.Produto.ProdutoId).ToList();
                editarProdutoViewModel.Fabricantes = _produtoRepositorio.Produtos.Where(p => p.Fabricante != null).Select(f => f.Fabricante).Distinct().OrderBy(f => f).ToList();
                editarProdutoViewModel.Categorias = _produtoRepositorio.Produtos.Select(c => c.Categoria).Distinct().OrderBy(c => c);
                return View(editarProdutoViewModel);
            }
        }
        public LocalRedirectResult ApagarImagem(int id, string url = null)
        {
            _imagensRepositorio.ApagarImagem(id);
            return LocalRedirect(url);
        }
        public ActionResult CriarProduto(EditarProdutoViewModel editarProdutoViewModel)
        {
            bool parametro = editarProdutoViewModel.Produto == null || editarProdutoViewModel == null;
            IEnumerable<string> categorias = _produtoRepositorio.Produtos.Select(c => c.Categoria).Distinct().OrderBy(c => c);
            IEnumerable<string> fabricantes = _produtoRepositorio.Produtos.Select(f => f.Fabricante).Distinct().OrderBy(f => f);
            if (parametro)
            {
                return View(new EditarProdutoViewModel
                {
                    Categorias = categorias,
                    Fabricantes = fabricantes
                });
            }
            ModelState.Remove("Imagem.ProdutoId");
            ModelState.Remove("Produto.ProdutoId");
            if (ModelState.IsValid)
            {
                Produto produto = editarProdutoViewModel.Produto;
                produto.Imagens = new List<Imagem>{
                    new Imagem {
                        ProdutoId = produto.ProdutoId,
                        Nome = editarProdutoViewModel.Imagem.Nome,
                        Link = editarProdutoViewModel.Imagem.Link
                    }
                };
                _produtoRepositorio.RegistrarProduto(editarProdutoViewModel.Produto);
                return RedirectToAction(nameof(Listar));
            }
            return View(new EditarProdutoViewModel { Categorias = categorias, Fabricantes = fabricantes });
        }
        [HttpPost]
        public IActionResult Apagar(int ID)
        {
            Produto deletedProduto = _produtoRepositorio.ApagarProduto(ID);
            if (deletedProduto != null)
            {
                TempData["message"] = $"{deletedProduto.Nome} foi apagado.";
            }
            return RedirectToAction("Listar");
        }
        public IActionResult ValidarComentarios(bool aprovado = false)
        {
            IEnumerable<Comentario> comentarios = _comentariosRepositorio.Comentarios.Where(c => c.Aprovado == aprovado);
            return View(comentarios);
        }

        public IActionResult AprovarComentario(int comentarioId)
        {
            Comentario comentario = _comentariosRepositorio.Comentarios.SingleOrDefault(c => c.ComentarioId == comentarioId);
            comentario.Aprovado = true;
            _comentariosRepositorio.RegistrarComentario(comentario);
            return RedirectToAction(nameof(ValidarComentarios));
        }
        [HttpGet("[controller]/[action]/{comentarioId}")]
        public IActionResult DetalhesComentario(int comentarioId)
        {
            Comentario comentario = _comentariosRepositorio.Comentarios.SingleOrDefault(c => c.ComentarioId == comentarioId);
            return View(comentario);
        }
        [HttpGet("[controller]/[action]/{comentarioId}")]
        public IActionResult RemoverComentario(int comentarioId)
        {
            Comentario comentario = _comentariosRepositorio.Comentarios.SingleOrDefault(c => c.ComentarioId == comentarioId);
            _comentariosRepositorio.ApagarComentario(comentario);
            TempData["massage"] = $"{comentario.NomeUsuario} apagado com sucesso.";
            return RedirectToAction(nameof(ValidarComentarios));
        }
    }
}
