using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreEP.Models;
using StoreEP.Infrastructure;
using StoreEP.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreEP.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly StoreEPContext _context;
        private Carrinho Carrinho;
        public CarrinhoController(StoreEPContext repo, Carrinho carService)
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
        public RedirectToActionResult AddToCart(int produtoID, string returnUrl)
        {
            Produto produto = _context.Produto.FirstOrDefault(p => p.ProdutoID == produtoID);
            if(produto != null)
            {
                Carrinho.AddItem(produto, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int produtoID, string returnUrl)
        {
            Produto produto = _context.Produto.FirstOrDefault(p => p.ProdutoID == produtoID);
            if (produto != null)
            {
                Carrinho.RemoveLine(produto);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
