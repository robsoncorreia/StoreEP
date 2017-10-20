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
        private IProdutoRepositorio _produtoRepositorio;
        private IImagensRepositorio _imagensRepositorio;
        private IComentariosRepositorio _comentariosRepositorio;

        public AdminController(
            IProdutoRepositorio repo, 
            IImagensRepositorio imagens, 
            IComentariosRepositorio comentariosRepositorio)
        {
            _produtoRepositorio = repo;
            _imagensRepositorio = imagens;
            _comentariosRepositorio = comentariosRepositorio;
        }
        public ViewResult Index() => View(new AdminIndexViewModel
        {
            NumeroProdutosRegistrados = _produtoRepositorio.Produtos.Count(),
            ComentariosNaoAprovados = _comentariosRepositorio.Comentarios
                                                             .Where(c => c.Aprovado == false)
                                                             .Count()
        });

        [HttpGet("[controller]/[action]/")]
        public ViewResult Listar() => View(_produtoRepositorio.Produtos);

        [AutoValidateAntiforgeryToken]
        public ViewResult EditarProduto(int ID)
        {
            IEnumerable<Imagem> imagens = _imagensRepositorio.Imagens.Where(i => i.ProdutoID == ID).ToList();
            Produto produto = _produtoRepositorio.Produtos.FirstOrDefault(p => p.ProdutoID == ID);
            IEnumerable<string> fabricantes = _produtoRepositorio.Produtos
                                                                 .Where(p => p.Fabricante != null)
                                                                 .Select(f => f.Fabricante)
                                                                 .OrderBy(f => f)
                                                                 .ToList();

            IEnumerable<string> categorias = _produtoRepositorio.Produtos
                                                                .Where(c => c.Categoria != null)
                                                                .Select(x => x.Categoria)
                                                                .Distinct()
                                                                .OrderBy(x => x);
            return View(new EditarProdutoViewModel
            {
                Imagens = imagens,
                Fabricantes = _produtoRepositorio.Produtos
                                                 .Where(p => p.Fabricante != null)
                                                 .Select(f => f.Fabricante)
                                                 .OrderBy(f => f).ToList(),
                Produto = produto,
                Categorias = categorias
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
                model.Imagens = _imagensRepositorio.Imagens.Where(i => i.ProdutoID == model.Produto.ProdutoID).ToList();
                model.Fabricantes = _produtoRepositorio.Produtos.Where(p => p.Fabricante != null)
                                                                .Select(f => f.Fabricante)
                                                                .Distinct()
                                                                .OrderBy(f => f)
                                                                .ToList();
                model.Categorias = _produtoRepositorio.Produtos.Where(p => p.Categoria != null)
                                                               .Select(x => x.Categoria)
                                                               .Distinct()
                                                               .OrderBy(x => x)
                                                               .ToList();
                return RedirectToAction("EditarProduto",ID);
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
                model.Imagens = _imagensRepositorio.Imagens
                                                   .Where(i => i.ProdutoID == model.Produto.ProdutoID)
                                                   .ToList();
                model.Fabricantes = _produtoRepositorio.Produtos
                                                       .Where(p => p.Fabricante != null)
                                                       .Select(f => f.Fabricante)
                                                       .Distinct().OrderBy(f => f)
                                                       .ToList();
                model.Categorias = _produtoRepositorio.Produtos.Where(p => p.Categoria != null)
                                                               .Select(c => c.Categoria)
                                                               .Distinct()
                                                               .OrderBy(c => c);
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
            IEnumerable<string> categorias = _produtoRepositorio.Produtos   
                                                                .Where(p => p.Categoria != null)
                                                                .Select(c => c.Categoria)
                                                                .Distinct()
                                                                .OrderBy(c => c);
            IEnumerable<string> fabricantes = _produtoRepositorio.Produtos
                                                                 .Where(p => p.Fabricante != null)
                                                                 .Select(f => f.Fabricante)
                                                                 .Distinct()
                                                                 .OrderBy(f => f);
            return View(new EditarProdutoViewModel { Categorias = categorias, Fabricantes = fabricantes });
        }
        [HttpPost]
        public ActionResult CriarProduto(EditarProdutoViewModel editarProdutoViewModel)
        {
            editarProdutoViewModel.Categorias = _produtoRepositorio.Produtos
                                                                   .Where(p => p.Categoria != null)
                                                                   .Select(c => c.Categoria)
                                                                   .Distinct()
                                                                   .OrderBy(c => c);
            editarProdutoViewModel.Fabricantes = _produtoRepositorio.Produtos
                                                                    .Where(p => p.Fabricante != null)
                                                                    .Select(f => f.Fabricante)
                                                                    .Distinct()
                                                                    .OrderBy(f => f);
            ModelState.Remove("Produto.ID");
            if (ModelState.IsValid)
            {
                editarProdutoViewModel.Produto.Imagens = new List<Imagem> { editarProdutoViewModel.Imagem };
                _produtoRepositorio.RegistrarProduto(editarProdutoViewModel.Produto);
                TempData["massage"] = $"{editarProdutoViewModel.Produto.Nome} foi salvo com sucesso.";
                return RedirectToAction(nameof(Listar));
            }
            return View(editarProdutoViewModel);
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
    }
}
