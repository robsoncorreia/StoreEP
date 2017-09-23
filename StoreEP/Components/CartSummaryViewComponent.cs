using Microsoft.AspNetCore.Mvc;
using StoreEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly Carrinho _cart;
        public CartSummaryViewComponent(Carrinho Carrinho)
        {
            _cart = Carrinho;
        }
        public IViewComponentResult Invoke()
        {
            return View(this._cart);
        }
    }
}
