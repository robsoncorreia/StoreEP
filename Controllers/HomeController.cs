using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreEP.Models;

namespace StoreEP.Controllers
{
    public class HomeController : Controller
    {
        SimpleReposity reposity =  SimpleReposity.SharedRepository;
        public IActionResult Index()
        {
            //Exemplo usando o metodo Where do Linq
            return View(SimpleReposity.SharedRepository.Produtos.Where(p => p.Preco >= 30));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult AdicionarProduto() => View(new Produto());

        [HttpPost]
        public IActionResult AdicionarProduto(Produto produto){
            reposity.AddProduto(produto);
            return RedirectToAction("Index");
        }
    }
}
