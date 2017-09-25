using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreEP.Models;

namespace StoreEP.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {

        private readonly IProdutoRepositorio _context;

        public NavigationMenuViewComponent(IProdutoRepositorio context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_context.Produtos.Select(x => x.CategoriaPD).Distinct().OrderBy(x => x));
        }
    }
}
