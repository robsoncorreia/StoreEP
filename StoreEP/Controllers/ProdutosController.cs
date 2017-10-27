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
using Microsoft.AspNetCore.Identity;

namespace StoreEP.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IImagensRepositorio _imagensRepositorio;
        private readonly IComentariosRepositorio _comentariosRepositorio;
        private readonly IHistoricoPrecosRepositorio _historicoPrecosRepositorio;
        private readonly IProdutoVisitadoRepositorio _produtoVisitadoRepositorio;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private StoreEPDbContext _dbContext;

        public int PageSize = 4;

        public ProdutosController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IProdutoVisitadoRepositorio produtoVisitadoRepositorio,
            IProdutoRepositorio produtoRepositorio,
            IImagensRepositorio imagensRepositorio,
            IComentariosRepositorio comentariosRepositorio,
            IHistoricoPrecosRepositorio historicoPrecosRepositorio,
            StoreEPDbContext dbContext
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _dbContext = dbContext;
            _produtoVisitadoRepositorio = produtoVisitadoRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _imagensRepositorio = imagensRepositorio;
            _historicoPrecosRepositorio = historicoPrecosRepositorio;
            _comentariosRepositorio = comentariosRepositorio;
        }

        public IActionResult Index() => View(new ProductsListViewModel
        {
            Produtos = _produtoRepositorio.Produtos.Where(p => p.Publicado == true).ToList(),
            Imagens = _imagensRepositorio.Imagens.ToList()
        });

        [HttpGet("[controller]/[action]/{fabricante}")]
        public ViewResult BuscarFabricante(string fabricante)
        {
            var produto = from p in _dbContext.Produtos
                          where p.Fabricante.Equals(fabricante) &&
                          p.Quantidade > 0 &&
                          p.Publicado == true
                          select p;

            var imagens = from i in _dbContext.Imagens
                          join p in _dbContext.Produtos
                          on i.ProdutoID equals p.ProdutoID
                          where p.Fabricante.Equals(fabricante) &&
                          p.Quantidade > 0 &&
                          p.Publicado == true
                          select i;

            return View("Listar", new ProductsListViewModel
            {
                Produtos = produto.ToList(),
                Imagens = imagens.ToList(),
                PagingInfo = new PagingInfo
                {
                    ItensPerPage = 4,
                    TotalItems = produto.Count(),
                    CurrentPage = 1
                }
            });
        }

        public async Task<IActionResult> Filtrar(FiltroViewModel model)
        {
            var produtos = from p in _dbContext.Produtos
                           select p;
            var imagens = from i in _dbContext.Imagens
                          select i;
            var produtosVisitados = _dbContext.ProdutosVisitados;

            switch (model.Filtro)
            {
                case "menor_preco":
                    produtos = from p in produtos
                               where p.Publicado == true && p.Quantidade > 0
                               orderby p.Preco
                               select p;
                    imagens = from i in imagens
                              join p in produtos
                              on i.ProdutoID equals p.ProdutoID
                              orderby p.Preco
                              select i;
                    break;
                case "maior_preco":
                    produtos = from p in produtos
                               where p.Publicado == true && p.Quantidade > 0
                               orderby p.Preco descending
                               select p;
                    imagens = from p in produtos
                              join i in imagens
                              on p.ProdutoID equals i.ProdutoID
                              orderby p.Preco descending
                              select i;
                    break;
                case "novos":
                    produtos = from p in produtos
                               where p.Publicado && p.Quantidade > 0
                               orderby p.DataCadastro descending
                               select p;
                    imagens = from i in imagens
                              join p in produtos
                              on i.ProdutoID equals p.ProdutoID
                              where p.Publicado == true && p.Quantidade > 0
                              select i;
                    break;
                case "recomendados":
                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                    }
                    else
                    {
                    }
                    break;
            }
            IEnumerable<Produto> modelProdutos = await produtos.AsNoTracking().ToListAsync();
            IEnumerable<Imagem> modelImagens = await imagens.AsNoTracking().ToListAsync();
            int numeroItens = modelProdutos.Count();
            ProductsListViewModel viewModel = new ProductsListViewModel
            {
                Produtos = modelProdutos,
                Imagens = modelImagens,
                PagingInfo = new PagingInfo
                {
                    TotalItems = numeroItens,
                    ItensPerPage = 4,
                    CurrentPage = 1
                }
            };
            return View("Listar", viewModel);
        }

        [HttpPost("[controller]/[action]/")]
        public IActionResult Buscar(BuscaViewModel modelBusca, int pagina = 1)
        {
            if (!ModelState.IsValid)
            {
                TempData["busca_nula"] = "Digite algo na campo busca.";
                return RedirectToAction(nameof(Listar));
            }
            IEnumerable<Produto> produtos = _produtoRepositorio.Produtos
                                                               .Where(p => p.Publicado == true && p.Nome.ToUpper()
                                                               .Contains(modelBusca.Texto.ToUpper()))
                                                               .OrderBy(p => p.ProdutoID)
                                                               .ToList();
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = pagina,
                ItensPerPage = 4,
                TotalItems = produtos.Count()
            };

            if (produtos.Count() == 1)
            {
                Produto produto = produtos.SingleOrDefault();
            }
            ProductsListViewModel model = new ProductsListViewModel
            {
                Produtos = produtos,
                Imagens = _imagensRepositorio.Imagens.ToList(),
                PagingInfo = pagingInfo
            };
            char s = model.Produtos.Count() < 2 ? ' ' : 's';
            ViewData["filtro.Nome"] = $"{model.Produtos.Count()} resultado{s} para a busca {modelBusca.Texto}.";
            return View("Listar", model);
        }

        public async Task<IActionResult> Listar(string category, int page = 1)
        {
            IEnumerable<Produto> produtosMaisVisitados = null;
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                produtosMaisVisitados = await _dbContext.ProdutosVisitados
                                                           .Where(p => p.UserID == user.Id)
                                                           .OrderByDescending(p => p.QuantidadeVisita)
                                                           .Select(p => p.Produto)
                                                           .Take(4)
                                                           .ToListAsync();
            }
            IEnumerable<Produto> produtos = await _dbContext.Produtos
                                                          .Where(p => (category == null || p.Categoria == category) && p.Publicado == true)
                                                          .OrderBy(p => p.ProdutoID)
                                                          .Skip((page - 1) * PageSize)
                                                          .Take(PageSize)
                                                          .ToListAsync();
            IEnumerable<string> categorias = await _dbContext.Produtos
                                                                 .Where(p => p.Categoria != null)
                                                                 .Select(p => p.Categoria)
                                                                 .Distinct()
                                                                 .ToListAsync();
            IEnumerable<Imagem> imagens = await _dbContext.Imagens.ToListAsync();

            ProductsListViewModel model = new ProductsListViewModel
            {
                ProdutosMaisVisitados = produtosMaisVisitados,
                Imagens = imagens,
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
            return View(model);
        }

        [HttpGet("[controller]/[action]/{ID}")]//https://docs.microsoft.com/pt-br/aspnet/core/mvc/controllers/routing
        public async Task<IActionResult> Detalhes(int ID)
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
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                _produtoVisitadoRepositorio.AdicionarProdutoVisitado(user.Id, produto);
            }
            return View(new DetalheProdutoViewModels
            {
                HistoricosPreco = _historicoPrecosRepositorio.HistoricosPreco.Where(h => h.ProdutoID == ID).ToList(),
                Produto = _produtoRepositorio.Produtos.SingleOrDefault(p => p.ProdutoID == ID),
                Imagens = _imagensRepositorio.Imagens.Where(i => i.ProdutoID == ID).ToList(),
                Comentarios = _comentariosRepositorio.Comentarios.Where(c => c.ProdutoID == ID && c.Aprovado == true).ToList()
            });
        }
    }
}
