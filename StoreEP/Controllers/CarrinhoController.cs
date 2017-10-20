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
        private Carrinho Carrinho;
        public CarrinhoController(StoreEPDbContext repo, Carrinho carService)
        {
            _lojaContexto = repo;
            Carrinho = carService;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel {
                Carrinho = Carrinho,
                ReturnUrl = returnUrl
            });
        }
        public RedirectToActionResult AdicionarCarrinho(int ID, string returnUrl)
        {
            Produto produto = _lojaContexto.Produtos.FirstOrDefault(p => p.ProdutoID == ID);
            if(produto != null)
            {
                Carrinho.AddItem(produto, 1);
            }
            return RedirectToAction("Finalizar", "Pedido", new { returnUrl });
        }
        public RedirectToActionResult RemoverCarrinho(int ID, string returnUrl)
        {
            Produto produto = _lojaContexto.Produtos.FirstOrDefault(p => p.ProdutoID == ID);
            if (produto != null)
            {
                Carrinho.RemoveLine(produto);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
