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
        public async Task<ViewResult> BuscarFabricante(string fabricante, int pagina = 1)
        {
            IEnumerable<Produto> produtos = _produtoRepositorio.Produtos
                                                                .Where(p => p.Fabricante == fabricante);
            IEnumerable<Produto> produtosVisitados = await GetProdutosVisitados();
            return View(nameof(Listar), new ProductsListViewModel
            {
                ProdutosMaisVisitados = produtosVisitados,
                Produtos = produtos,
                PagingInfo = new PagingInfo
                {
                    ItensPerPage = itensPorPagina,
                    TotalItems = produtos.Count(),
                    CurrentPage = pagina
                }
            });
        }

        public async Task<IActionResult> Ordenar(int ordena, int page = 1)
        {
            IEnumerable<Produto> produtos = _produtoRepositorio.Produtos
                                                                .Where(p => p.Quantidade > 0 &&
                                                                       p.Publicado == true)
                                                                .ToList();
            int numeroProdutos = produtos.Count();

            IEnumerable<Produto> produtosVisitados = await GetProdutosVisitados();

            switch (ordena)
            {
                case 0:
                    produtos = produtos.Where(p => p.Publicado == true && p.Quantidade > 0)
                                        .OrderBy(p => p.Preco)
                                        .ToList();
                    break;
                case 1:
                    produtos = produtos.Where(p => p.Publicado == true && p.Quantidade > 0)
                                        .OrderByDescending(p => p.Preco)
                                        .ToList();
                    break;
                case 2:
                    produtos = produtos.Where(p => p.Publicado == true && p.Quantidade > 0)
                                        .OrderByDescending(p => p.DataCadastro)
                                        .ToList();
                    break;
                case 3:

                    break;
            }
            ProductsListViewModel viewModel = new ProductsListViewModel
            {
                ProdutosMaisVisitados = produtosVisitados,
                Produtos = produtos.Skip((page - 1) * itensPorPagina)
                                    .Take(itensPorPagina),
                PagingInfo = new PagingInfo
                {
                    TotalItems = numeroProdutos,
                    ItensPerPage = itensPorPagina,
                    CurrentPage = page
                }
            };
            return View(nameof(Listar), viewModel);
        }

        [HttpPost("[controller]/[action]/")]
        public async Task<IActionResult> Buscar(BuscaViewModel modelBusca, int pagina = 1)
        {
            if (!ModelState.IsValid)
            {
                TempData["busca_nula"] = "Digite algo na campo busca.";
                return RedirectToAction(nameof(Listar));
            }
            IEnumerable<Produto> produtoVisitado = await GetProdutosVisitados();
            IEnumerable<Produto> produtos = _produtoRepositorio.Produtos
                                                               .Where(p => p.Publicado == true && p.Nome.ToUpper()
                                                               .Contains(modelBusca.Texto.ToUpper()))
                                                               .OrderBy(p => p.ProdutoID)
                                                               .ToList();
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = pagina,
                ItensPerPage = itensPorPagina,
                TotalItems = produtos.Count()
            };

            if (produtos.Count() == 1)
            {
                Produto produto = produtos.SingleOrDefault();
            }
            ProductsListViewModel model = new ProductsListViewModel
            {
                ProdutosMaisVisitados = produtoVisitado,
                Produtos = produtos,
                PagingInfo = pagingInfo
            };
            char s = model.Produtos.Count() < 2 ? ' ' : 's';
            ViewData["filtro.Nome"] = $"{model.Produtos.Count()} resultado{s} para a busca {modelBusca.Texto}.";
            return View(nameof(Listar), model);
        }

        public async Task<IActionResult> Listar(string category, int page = 1)
        {
            IEnumerable<Produto> produtosMaisVisitados = await GetProdutosVisitados();

            IEnumerable<Produto> produtos = _produtoRepositorio.Produtos
                                                          .Where(p => (category == null || p.Categoria == category) && p.Publicado == true && p.Quantidade > 0)
                                                          .OrderBy(p => p.ProdutoID);
            int numeroProdutos = produtos.Count();
            produtos = produtos.Skip((page - 1) * itensPorPagina)
                                .Take(itensPorPagina)
                                .ToList();
            IEnumerable<string> categorias = _produtoRepositorio.Produtos
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
                    TotalItems = numeroProdutos
                },
                CurrentCategory = category
            };
            return View(model);
        }

        [HttpGet("[controller]/[action]/{ID}")]
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
        public async Task<IEnumerable<Produto>> GetProdutosVisitados()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                return _produtoVisitadoRepositorio.ProdutosVisitados
                                                           .Where(p => p.UserID == user.Id)
                                                           .OrderByDescending(p => p.QuantidadeVisita)
                                                           .Select(p => p.Produto)
                                                           .Take(4)
                                                           .ToList();
            }
            return null;
        }
    }
}
