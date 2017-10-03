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
                _produtoRepositorio.RegistrarProduto(editarProdutoViewModel.Produto);
                TempData["massage"] = $"{editarProdutoViewModel.Produto.Nome} foi salvo com sucesso.";
                return RedirectToAction(nameof(Listar));
            }
            else
            {
                return View(editarProdutoViewModel);
            }
        }
        public ViewResult CriarProduto() => View(new EditarProdutoViewModel
        {
            Categorias = _produtoRepositorio.Produtos.Select(c => c.Categoria).Distinct().OrderBy(c => c)
        });
        public ActionResult CriarProduto(EditarProdutoViewModel editarProdutoViewModel)
        {
            _produtoRepositorio.RegistrarProduto(editarProdutoViewModel.Produto);
            return RedirectToAction(nameof(Listar));
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
