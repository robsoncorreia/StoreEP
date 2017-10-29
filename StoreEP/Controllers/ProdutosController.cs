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
        private readonly IHistoricoPrecosRepositorio _historicoPrecosRepositorio;
        private readonly IProdutoVisitadoRepositorio _produtoVisitadoRepositorio;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public int itensPorPagina = 4;

        public ProdutosController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IProdutoVisitadoRepositorio produtoVisitadoRepositorio,
            IProdutoRepositorio produtoRepositorio,
            IHistoricoPrecosRepositorio historicoPrecosRepositorio
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _produtoVisitadoRepositorio = produtoVisitadoRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _historicoPrecosRepositorio = historicoPrecosRepositorio;
        }


        [HttpGet("[controller]/[action]/{fabricante}")]
        public ViewResult BuscarFabricante(string fabricante,int pagina = 1)
        {
            IEnumerable<Produto> produtos = _produtoRepositorio.Produtos
                                                        .Where(p => p.Fabricante
                                                        .Equals(fabricante) && p.Quantidade > 0)
                                                        .ToList();

            return View("Listar", new ProductsListViewModel
            {
                Produtos = produtos,
                PagingInfo = new PagingInfo
                {
                    ItensPerPage = itensPorPagina,
                    TotalItems = produtos.Count(),
                    CurrentPage = pagina
                }
            });
        }

        public IActionResult Filtrar(FiltroViewModel model, int page = 1)
        {
            IEnumerable<Produto> produtos = _produtoRepositorio.Produtos
                                                                .ToList();

            IEnumerable<ProdutoVisitado> produtosVisitados = _produtoVisitadoRepositorio.ProdutosVisitados.ToList();

            switch (model.Filtro)
            {
                case "menor_preco":
                    produtos = produtos.Where(p => p.Publicado == true && p.Quantidade > 0)
                                        .OrderBy(p => p.Preco)
                                        .ToList();
                    break;
                case "maior_preco":
                    produtos = produtos.Where(p => p.Publicado == true && p.Quantidade > 0)
                                        .OrderByDescending(p => p.Preco)
                                        .ToList();
                    break;
                case "novos":
                    produtos = produtos.Where(p => p.Publicado == true && p.Quantidade > 0)
                                        .OrderByDescending(p => p.DataCadastro)
                                        .ToList();
                    break;
                case "recomendados":

                    break;
            }
            ProductsListViewModel viewModel = new ProductsListViewModel
            {
                Produtos = produtos.ToList(),
                PagingInfo = new PagingInfo
                {
                    TotalItems = produtos.Count(),
                    ItensPerPage = itensPorPagina,
                    CurrentPage = page
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
                produtosMaisVisitados =  _produtoVisitadoRepositorio.ProdutosVisitados
                                                           .Where(p => p.UserID == user.Id)
                                                           .OrderByDescending(p => p.QuantidadeVisita)
                                                           .Select(p => p.Produto)
                                                           .Take(4)
                                                           .ToList();
            }
            IEnumerable<Produto> produtos = _produtoRepositorio.Produtos
                                                          .Where(p => (category == null || p.Categoria == category) && p.Publicado == true)
                                                          .OrderBy(p => p.ProdutoID)
                                                          .Skip((page - 1) * itensPorPagina)
                                                          .Take(itensPorPagina)
                                                          .ToList();
            IEnumerable<string> categorias =  _produtoRepositorio.Produtos
                                                                 .Where(p => p.Categoria != null)
                                                                 .Select(p => p.Categoria)
                                                                 .Distinct()
                                                                 .ToList();

            ProductsListViewModel model = new ProductsListViewModel
            {
                ProdutosMaisVisitados = produtosMaisVisitados,
                Categorias = categorias,
                Produtos = produtos,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItensPerPage = itensPorPagina,
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
            Produto produto = _produtoRepositorio.Produtos
                                        .SingleOrDefault(p => p.ProdutoID == ID);
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
                Produto = produto
            });
        }
    }
}
