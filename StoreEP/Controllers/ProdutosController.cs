using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreEP.Models;
using StoreEP.Models.ViewModels;
using StoreEP.Models.Interface;

namespace StoreEP.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IImagensRepositorio _imagensRepositorio;
        private readonly IComentariosRepositorio _comentariosRepositorio;

        public int PageSize = 4;

        public ProdutosController(IProdutoRepositorio produtoRepositorio, IImagensRepositorio imagensRepositorio, IComentariosRepositorio comentariosRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
            _imagensRepositorio = imagensRepositorio;
            _comentariosRepositorio = comentariosRepositorio;
        }

        [HttpPost]
        public IActionResult Index(string busca) => View(new ProductsListViewModel
        {
            Produtos = _produtoRepositorio.Produtos.Where(p => p.Publicado == true).ToList(),
            Imagens =  _imagensRepositorio.Imagens.ToList()
        });
        //GET: Produtos
        public IActionResult Index() => View(new ProductsListViewModel
        {
            Produtos = _produtoRepositorio.Produtos.Where(p => p.Publicado == true).ToList(),
            Imagens = _imagensRepositorio.Imagens.ToList()
        });


        //// GET: Produtos/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Produtos/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,Nome,Categoria,Preco,Descricao,LinkImagemPD,Fabricante")] Produto produto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _lojaContexto.Add(produto);
        //        await _lojaContexto.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(produto);
        //}

        //// GET: Produtos/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var produto = await _lojaContexto.Produtos.SingleOrDefaultAsync(p => p.ProdutoId == id);
        //    if (produto == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(produto);
        //}

        //// POST: Produtos/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,Categoria,Preco,Descricao,LinkImagemPD,Fabricante")] Produto produto)
        //{
        //    if (id != produto.ProdutoId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _lojaContexto.Update(produto);
        //            await _lojaContexto.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProdutoExists(produto.ProdutoId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(produto);
        //}

        //// GET: Produtos/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var produto = await _lojaContexto.Produtos
        //        .SingleOrDefaultAsync(p => p.ProdutoId == id);
        //    if (produto == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(produto);
        //}



        //private bool ProdutoExists(int id)
        //{
        //    return _lojaContexto.Produtos.Any(p => p.ProdutoId == id);
        //}
        public IActionResult Buscar(Filtro filtro)
        {
            ProductsListViewModel productsListViewModel = new ProductsListViewModel
            {
                Produtos = _produtoRepositorio.Produtos.Where(p => p.Publicado == true && p.Nome.Contains(filtro.Nome)).OrderBy(p => p.ProdutoId).Skip((0) * PageSize).Take(PageSize).ToList(),
                Imagens = _imagensRepositorio.Imagens.ToList(),
                PagingInfo = new PagingInfo
                {
                    ItensPerPage = PageSize,
                    TotalItems = _produtoRepositorio.Produtos.Count()
                }
            };
            char s = productsListViewModel.Produtos.Count() < 2 ? ' ' : 's';
            ViewData["filtro.Nome"] = $"{productsListViewModel.Produtos.Count()} resultado{s} para a busca {filtro.Nome}.";
            return View("Listar",productsListViewModel);
        }

        public IActionResult Listar(string category, int page = 1)
        {
            ProductsListViewModel productsListViewModel = new ProductsListViewModel
            {
                Produtos = _produtoRepositorio.Produtos.Where(p => (category == null || p.Categoria == category) && p.Publicado == true).OrderBy(p => p.ProdutoId).Skip((page - 1) * PageSize).Take(PageSize).ToList(),
                Imagens =  _imagensRepositorio.Imagens.ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItensPerPage = PageSize,
                    TotalItems = _produtoRepositorio.Produtos.Count()
                },
                CurrentCategory = category
            };
            return View(productsListViewModel);
        }

        [HttpGet("[controller]/[action]/{produtoid}")]//https://docs.microsoft.com/pt-br/aspnet/core/mvc/controllers/routing
        public IActionResult Detalhes(int? produtoid)
        {
            if (produtoid == null)
            {
                return NotFound();
            }
            Produto produto = _produtoRepositorio.Produtos.SingleOrDefault(p => p.ProdutoId == produtoid);
            if (produto == null)
            {
                return NotFound();
            }
            return View(new DetalheProdutoViewModels
            {
                Produto = _produtoRepositorio.Produtos.SingleOrDefault(p => p.ProdutoId == produtoid),
                Imagens = _imagensRepositorio.Imagens.Where(i => i.ProdutoId == produtoid).ToList(),
                Comentarios = _comentariosRepositorio.Comentarios.Where(c => c.ProdutoId == produtoid && c.Aprovado == true).ToList()
            });
        }
    }
}
