using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreEP.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreEP.Controllers
{
    public class HomeController : Controller
    {

        private readonly StoreEPContext _context;

        public HomeController(StoreEPContext context) => _context = context;

        // GET: Produtos
        public async Task<IActionResult> Index() => View(await _context.Produto.ToListAsync());

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
            return View();
        }
    }
}
