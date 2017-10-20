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

        public ProdutosController(
            IProdutoRepositorio produtoRepositorio, 
            IImagensRepositorio imagensRepositorio, 
            IComentariosRepositorio comentariosRepositorio
            )
        {
            _produtoRepositorio = produtoRepositorio;
            _imagensRepositorio = imagensRepositorio;
            _comentariosRepositorio = comentariosRepositorio;
        }

        [HttpPost]
        public IActionResult Index(string busca) => View(new ProductsListViewModel
        {
            Produtos = _produtoRepositorio.Produtos.Where(p => p.Publicado == true).ToList(),
            Imagens = _imagensRepositorio.Imagens.ToList()
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

        //    var produto = await _lojaContexto.Produtos.SingleOrDefaultAsync(p => p.ID == id);
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
        //    if (id != produto.ID)
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
        //            if (!ProdutoExists(produto.ID))
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
        //        .SingleOrDefaultAsync(p => p.ID == id);
        //    if (produto == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(produto);
        //}



        //private bool ProdutoExists(int id)
        //{
        //    return _lojaContexto.Produtos.Any(p => p.ID == id);
        //}

        public IActionResult Buscar(Filtro filtro, int pagina = 1)
        {
            IEnumerable<Produto> produtos = _produtoRepositorio.Produtos.Where(p => p.Publicado == true && p.Nome.ToUpper().Contains(filtro.Nome.ToUpper())).OrderBy(p => p.ProdutoID).Skip((0) * PageSize).Take(PageSize).ToList();
            if (produtos.Count() == 1)
            {
                Produto produto = produtos.SingleOrDefault();
            }
            ProductsListViewModel model = new ProductsListViewModel
            {
                Produtos = produtos,
                Imagens = _imagensRepositorio.Imagens.ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pagina,
                    ItensPerPage = PageSize,
                    TotalItems = _produtoRepositorio.Produtos.Count()
                }
            };
            char s = model.Produtos.Count() < 2 ? ' ' : 's';
            ViewData["filtro.Nome"] = $"{model.Produtos.Count()} resultado{s} para a busca {filtro.Nome}.";
            return View("Listar", model);
        }

        public IActionResult BuscarFabricante(string fabricante, int pagina = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Produtos = _produtoRepositorio.Produtos.Where(p => (fabricante == null || p.Fabricante == fabricante) && p.Publicado == true).OrderBy(p => p.ProdutoID).Skip((pagina - 1) * PageSize).Take(PageSize).ToList(),
                Imagens = _imagensRepositorio.Imagens.ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pagina,
                    ItensPerPage = PageSize,
                    TotalItems = _produtoRepositorio.Produtos.Count()
                },
                CurrentCategory = fabricante
            };
            return View("Listar", model);
        }

        public IActionResult Listar(string category, int page = 1)
        {
            IEnumerable<Produto> produtos = _produtoRepositorio.Produtos
                                                          .Where(p => (category == null || p.Categoria == category) && p.Publicado == true)
                                                          .OrderBy(p => p.ProdutoID)
                                                          .Skip((page - 1) * PageSize)
                                                          .Take(PageSize)
                                                          .ToList();
            IEnumerable<string> categorias = _produtoRepositorio.Produtos
                                                                 .Where(p => p.Categoria != null)
                                                                 .Select(p => p.Categoria)
                                                                 .Distinct()
                                                                 .ToList();

            ProductsListViewModel productsListViewModel = new ProductsListViewModel
            {
                Categorias = categorias,
                Produtos = produtos,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItensPerPage = PageSize,
                    TotalItems = category == null ? _produtoRepositorio.Produtos.Count() : _produtoRepositorio.Produtos.Where(p => (category == null || p.Categoria == category) && p.Publicado == true).Count()
                },
                CurrentCategory = category
            };
            return View(productsListViewModel);
        }

        [HttpGet("[controller]/[action]/{ID}")]//https://docs.microsoft.com/pt-br/aspnet/core/mvc/controllers/routing
        public IActionResult Detalhes(int ID)
        {
            if (ID == 0)
            {
                return NotFound();
            }
            Produto produto = _produtoRepositorio.Produtos.SingleOrDefault(p => p.ProdutoID == ID);
            if (produto == null)
            {
                return NotFound();
            }
            return View(new DetalheProdutoViewModels
            {
                Produto = _produtoRepositorio.Produtos.SingleOrDefault(p => p.ProdutoID == ID),
                Imagens = _imagensRepositorio.Imagens.Where(i => i.ProdutoID == ID).ToList(),
                Comentarios = _comentariosRepositorio.Comentarios.Where(c => c.ProdutoID == ID && c.Aprovado == true).ToList()
            });
        }
    }
}
