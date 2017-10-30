using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StoreEP.Models
{
    public class EFPedidoRepositorio : IPedidoRepositorio
    {
        private StoreEPDbContext context;
        public EFPedidoRepositorio(StoreEPDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Pedido> Pedidos => context.Pedidos
                                                        .Include(o => o.Lines)
                                                            .ThenInclude(p => p.Produto)
                                                                .ThenInclude(i => i.Imagens)
                                                        .Include(e => e.Endereco)
                                                        .Include(p => p.Pagamento)
                                                        .ToList();
        public void Registrar(Pedido pedido)
        {

            context.AttachRange(pedido.Lines.Select(i => i.Produto));
            if (pedido.PedidoID == 0)
            {
                context.Pedidos.Add(pedido);
            }
            var mensagem = context.SaveChanges();
        }
    }
}
