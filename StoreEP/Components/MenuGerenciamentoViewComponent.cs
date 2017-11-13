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
            var produtosNaoEnviados = _pedidoRepositorio.Pedidos
                                                        .Where(p => p.Enviado == false)
                                                        .Count();
            var numeroProdutosRegistrados = _produtoRepositorio.Produtos
                                                                .Count();
            var comentariosNaoAprovados = _avaliacoesRepositorio.Avaliacoes
                                                             .Where(c => c.Aprovado == false)
                                                             .Count();

            return View(new AdminIndexViewModel
            {
                PedidosNaoEnviados = produtosNaoEnviados,
                NumeroProdutosRegistrados = numeroProdutosRegistrados,
                ComentariosNaoAprovados = comentariosNaoAprovados
            });
        }
    }
}
