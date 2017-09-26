using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreEP.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreEP.Controllers
{
    public class AdminController : Controller
    {
        private IProdutoRepositorio repository;

        public AdminController(IProdutoRepositorio repo)
        {
            repository = repo;
        }

        public ViewResult List() => View(repository.Produtos);

        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Administrador")]
        public ViewResult Edit(int batata) => View(repository.Produtos.FirstOrDefault(p => p.ProdutoID == batata));

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public IActionResult Edit(Produto produto)
        {
            if (ModelState.IsValid)
            {
                repository.RegistrarProduto(produto);
                TempData["massage"] = $"{produto.NomePD} foi salvo com sucesso.";
                return RedirectToAction(nameof(List));
            }
            else
            {
                return View(produto);
            }
        }
        [Authorize(Roles = "Administrador")]
        public ViewResult Create() => View("Edit", new Produto());
        [HttpPost]
        public IActionResult Delete(int produtoId)
        {
            Produto deletedProduto = repository.ApagarProduto(produtoId);
            if (deletedProduto != null)
            {
                TempData["message"] = $"{deletedProduto.NomePD} foi apagado.";
            }
            return RedirectToAction("List");
        }
    }
}
