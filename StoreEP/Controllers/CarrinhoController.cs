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
        private readonly StoreEPDbContext _lojaContexto;
        private Carrinho _carrinho;
        public CarrinhoController(StoreEPDbContext repo, Carrinho carService)
        {
            _lojaContexto = repo;
            _carrinho = carService;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel {
                Carrinho = _carrinho,
                ReturnUrl = returnUrl
            });
        }
        public RedirectToActionResult Adicionar(int produtoID, string returnUrl)
        {
            Produto produto = _lojaContexto.Produtos.FirstOrDefault(p => p.ProdutoID == produtoID);
            if(produto != null)
            {
                int emEstoque = produto.Quantidade;
                _carrinho.AddItem(produto, 1);
            }
            return RedirectToAction("Finalizar", "Pedido", new { returnUrl });
        }
        public RedirectToActionResult RemoverCarrinho(int ID, string returnUrl)
        {
            Produto produto = _lojaContexto.Produtos.FirstOrDefault(p => p.ProdutoID == ID);
            if (produto != null)
            {
                _carrinho.RemoveLine(produto);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
