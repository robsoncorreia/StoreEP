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

        public IEnumerable<Pedido> Pedidos => context.Pedidos.Include(o => o.Lines).ThenInclude(l => l.Produto);

        public void SaveOrder(Pedido pedido)
        {
            context.AttachRange(pedido.Lines.Select(i => i.Produto));
            if (pedido.PedidoId == 0)
            {
                context.Pedidos.Add(pedido);
            }
            var mensagem = context.SaveChanges();
         }
    }
}
