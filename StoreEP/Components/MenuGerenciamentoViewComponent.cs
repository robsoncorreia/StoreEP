using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreEP.Models;
using StoreEP.Models.ViewModels;
using StoreEP.Models.Interface;

namespace StoreEP.Components
{
    public class MenuGerenciamentoViewComponent : ViewComponent
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IAvaliacaoRepositorio _avaliacoesRepositorio;
        private readonly IPedidoRepositorio _pedidoRepositorio;

        public MenuGerenciamentoViewComponent(
            IProdutoRepositorio produtoRepositorio,
            IAvaliacaoRepositorio comentariosRepositorio,
            IPedidoRepositorio pedidoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _avaliacoesRepositorio = comentariosRepositorio;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.OpcaoSelecionada = RouteData?.Values["opcaoSelecionada"];
            return View(new AdminIndexViewModel
            {
                ProdutosNãoEnviados = _pedidoRepositorio.Pedidos
                                                        .Where(p => p.Enviado == false)
                                                        .Count(),
                NumeroProdutosRegistrados = _produtoRepositorio.Produtos.Count(),
                ComentariosNaoAprovados = _avaliacoesRepositorio.Avaliacoes
                                                             .Where(c => c.Aprovado == false)
                                                             .Count()

            });
        }
    }
}
