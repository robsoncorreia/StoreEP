using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreEP.Models;
using StoreEP.Infrastructure;
using StoreEP.Models.ViewModels;

namespace StoreEP.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private Carrinho _carrinho;
        public CarrinhoController(
            IProdutoRepositorio produtoRepositorio,
            Carrinho carService)
        {
            _produtoRepositorio = produtoRepositorio;
            _carrinho = carService;
        }

        [HttpGet]
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Carrinho = _carrinho,
                ReturnUrl = returnUrl != null ? returnUrl : "/"
            });
        }

        [HttpPost]
        public int Adicionar(AddCarrinhoViewModel model)
        {
            int quantidade = model.QuantidadeProduto;
            int quantidadeEmEstoque = _produtoRepositorio.Produtos
                                                        .Where(p => p.ProdutoID == model.ProdutoID)
                                                        .Select(p => p.Quantidade)
                                                        .Sum();
            int quantidadeNoCarrinho = _carrinho.Lines
                                                .Where(p => p.Produto.ProdutoID == model.ProdutoID)
                                                .Select(p => p.Quantidade)
                                                .Sum();
            Produto produto = _produtoRepositorio.Produtos.FirstOrDefault(p => p.ProdutoID == model.ProdutoID);
            bool produtoEmEstoque = quantidadeEmEstoque >= quantidadeNoCarrinho && 
                                    quantidadeEmEstoque >= model.QuantidadeProduto;
            if (produto != null)
            {
                if (produtoEmEstoque)
                {
                    _carrinho.AddItem(produto, quantidade > 1 ? quantidade : 1);
                }
                else
                {
                    TempData["fora_estoque"] = $"{produto.Nome} fora de estoque.";
                    return -1;
                }
            }
            return _carrinho.QuantidadeTotal();

        }
        public IActionResult RemoverCarrinho(int produtoID, string returnUrl)
        {
            Produto produto = _produtoRepositorio.Produtos.FirstOrDefault(p => p.ProdutoID == produtoID);
            if (produto != null)
            {
                _carrinho.RemoveLine(produto);
            }
            return Redirect(returnUrl);
        }

        #region Utilidades
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(ProdutosController.Listar), "Produtos");
            }
        }
        #endregion
    }
}
