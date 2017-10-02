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

        private readonly IProdutoRepositorio _lojaContexto;

        public NavigationMenuViewComponent(IProdutoRepositorio context)
        {
            _lojaContexto = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_lojaContexto.Produtos.Select(x => x.Categoria).Distinct().OrderBy(x => x));
        }
    }
}
