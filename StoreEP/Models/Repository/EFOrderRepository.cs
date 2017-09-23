using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StoreEP.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private StoreEPContext context;

        public EFOrderRepository(StoreEPContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Pedido> Orders => context.Orders.Include(o => o.Lines).ThenInclude(l => l.Produto);

        public void SaveOrder(Pedido order)
        {
            context.AttachRange(order.Lines.Select(i => i.Produto));
            if (order.ID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
