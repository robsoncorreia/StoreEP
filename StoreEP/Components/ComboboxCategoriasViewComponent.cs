using Microsoft.AspNetCore.Mvc;
using StoreEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Components
{
    public class ComboboxCategoriasViewComponent : ViewComponent
    {
        private readonly IProdutoRepositorio _produtoRepositorio;

        public ComboboxCategoriasViewComponent(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }
        private IEnumerable<string> Categorias => _produtoRepositorio.Produtos
            .Where(p => p.Publicado == true && p.Quantidade > 0)
            .Select(p => p.Categoria)
            .Distinct();
        public IViewComponentResult Invoke()
        {
            return View(this.Categorias);
        }
    }
}
