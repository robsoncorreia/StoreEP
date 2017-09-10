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
        private readonly Cart _cart;
        public CartSummaryViewComponent(Cart cart)
        {
            _cart = cart;
        }
        public IViewComponentResult Invoke()
        {
            return View(this._cart);
        }
    }
}
