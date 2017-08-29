using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreEP.Models;
using StoreEP.Controllers;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreEP.Controllers
{
    public class ProdutoController : Controller
    {
        private IProdutoRepositorio produtoRepositorio;
        public int PageSize = 4;
        // GET: /<controller>/

        public ViewResult List(int page = 1) => View();
    }
}
