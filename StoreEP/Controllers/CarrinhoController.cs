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
        private readonly StoreEPDbContext _context;
        private Carrinho Carrinho;
        public CarrinhoController(StoreEPDbContext repo, Carrinho carService)
        {
            _context = repo;
            Carrinho = carService;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel {
                Carrinho = Carrinho,
                ReturnUrl = returnUrl
            });
        }
        public RedirectToActionResult AdicionarCarrinho(int produtoID, string returnUrl)
        {
            Produto produto = _context.Produtos.FirstOrDefault(p => p.ProdutoID == produtoID);
            if(produto != null)
            {
                Carrinho.AddItem(produto, 1);
            }
            return RedirectToAction("Finalizar", "Pedido", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int produtoID, string returnUrl)
        {
            Produto produto = _context.Produtos.FirstOrDefault(p => p.ProdutoID == produtoID);
            if (produto != null)
            {
                Carrinho.RemoveLine(produto);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}