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
        //GET: Produtos
        public async Task<IActionResult> Index() => View(await _lojaContexto.Produtos.ToListAsync());

        public async Task<IActionResult> List(string category, int page = 1) => View(new ProductsListViewModel
        {
            Produtos = await _lojaContexto.Produtos.Where(p => category == null || p.CategoriaPD == category).OrderBy(p => p.ProdutoID).Skip((page - 1) * PageSize).Take(PageSize).ToListAsync(),
            PagingInfo = new PagingInfo 
            {
                CurrentPage = page,
                ItensPerPage = PageSize,
                TotalItems = _lojaContexto.Produtos.Count()
            },
            CurrentCategory = category
        });

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _lojaContexto.Produtos
                .SingleOrDefaultAsync(m => m.ProdutoID == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

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
        public async Task<IActionResult> Create([Bind("ProdutoID,NomePD,CategoriaPD,PrecoPD,DescricaoPD,LinkImagemPD,Fabricante")] Produto produto)
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

            var produto = await _lojaContexto.Produtos.SingleOrDefaultAsync(m => m.ProdutoID == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("ProdutoID,NomePD,CategoriaPD,PrecoPD,DescricaoPD,LinkImagemPD,Fabricante")] Produto produto)
        {
            if (id != produto.ProdutoID)
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
                    if (!ProdutoExists(produto.ProdutoID))
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
                .SingleOrDefaultAsync(m => m.ProdutoID == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _lojaContexto.Produtos.SingleOrDefaultAsync(m => m.ProdutoID == id);
            _lojaContexto.Produtos.Remove(produto);
            await _lojaContexto.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool ProdutoExists(int id)
        {
            return _lojaContexto.Produtos.Any(e => e.ProdutoID == id);
        }
        [HttpGet("[controller]/[action]/{produtoid}")]
        public async Task<IActionResult> Detalhes(int produtoid)
        {
            Produto produto = await _lojaContexto.Produtos.SingleOrDefaultAsync(p => p.ProdutoID == produtoid);
            return View(produto);
        }
    }
}
