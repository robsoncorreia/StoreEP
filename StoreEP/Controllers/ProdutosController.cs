using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreEP.Models;
using StoreEP.Models.ViewModels;

namespace StoreEP.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly StoreEPDbContext _lojaContexto;

        public ProdutosController(StoreEPDbContext context)
        {
            _lojaContexto = context;
        }

        public int PageSize = 4;
        [HttpPost]
        public async Task<IActionResult> Index(string busca) => View(new ProductsListViewModel
        {
            Produtos = await _lojaContexto.Produtos.Where(p => p.Publicado == true).ToListAsync(),
            Imagens = await _lojaContexto.Imagens.ToListAsync()
        });
        //GET: Produtos
        public async Task<IActionResult> Index() => View(new ProductsListViewModel
        {
            Produtos = await _lojaContexto.Produtos.Where(p => p.Publicado == true).ToListAsync(),
            Imagens = await _lojaContexto.Imagens.ToListAsync()
        });


        // GET: Produtos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,Categoria,Preco,Descricao,LinkImagemPD,Fabricante")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _lojaContexto.Add(produto);
                await _lojaContexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _lojaContexto.Produtos.SingleOrDefaultAsync(p => p.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,Categoria,Preco,Descricao,LinkImagemPD,Fabricante")] Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _lojaContexto.Update(produto);
                    await _lojaContexto.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.ProdutoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _lojaContexto.Produtos
                .SingleOrDefaultAsync(p => p.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }



        private bool ProdutoExists(int id)
        {
            return _lojaContexto.Produtos.Any(p => p.ProdutoId == id);
        }
        public async Task<IActionResult> Buscar(Filtro filtro)
        {
            ProductsListViewModel productsListViewModel = new ProductsListViewModel
            {
                Produtos = await _lojaContexto.Produtos.Where(p => p.Publicado == true && p.Nome.Contains(filtro.Nome)).OrderBy(p => p.ProdutoId).Skip((0) * PageSize).Take(PageSize).ToListAsync(),
                Imagens = await _lojaContexto.Imagens.ToListAsync(),
                PagingInfo = new PagingInfo
                {
                    ItensPerPage = PageSize,
                    TotalItems = _lojaContexto.Produtos.Count()
                }
            };
            char s = productsListViewModel.Produtos.Count() < 2 ? ' ' : 's';
            ViewData["filtro.Nome"] = $"{productsListViewModel.Produtos.Count()} resultado{s} para a busca {filtro.Nome}.";
            return View("Listar",productsListViewModel);
        }

        public async Task<IActionResult> Listar(string category, int page = 1)
        {
            ProductsListViewModel productsListViewModel = new ProductsListViewModel
            {
                Produtos = await _lojaContexto.Produtos.Where(p => (category == null || p.Categoria == category) && p.Publicado == true).OrderBy(p => p.ProdutoId).Skip((page - 1) * PageSize).Take(PageSize).ToListAsync(),
                Imagens = await _lojaContexto.Imagens.ToListAsync(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItensPerPage = PageSize,
                    TotalItems = _lojaContexto.Produtos.Count()
                },
                CurrentCategory = category
            };
            return View(productsListViewModel);
        }

        [HttpGet("[controller]/[action]/{produtoid}")]//https://docs.microsoft.com/pt-br/aspnet/core/mvc/controllers/routing
        public async Task<IActionResult> Detalhes(int? produtoid)
        {
            if (produtoid == null)
            {
                return NotFound();
            }
            Produto produto = await _lojaContexto.Produtos.SingleOrDefaultAsync(p => p.ProdutoId == produtoid);
            if (produto == null)
            {
                return NotFound();
            }
            return View(new DetalheProdutoViewModels
            {
                Produto = await _lojaContexto.Produtos.SingleOrDefaultAsync(p => p.ProdutoId == produtoid),
                Imagens = await _lojaContexto.Imagens.Where(i => i.ProdutoId == produtoid).ToListAsync(),
                Comentarios = await _lojaContexto.Comentarios.Where(c => c.ProdutoId == produtoid && c.Aprovado == true).ToListAsync()
            });
        }
    }
}
