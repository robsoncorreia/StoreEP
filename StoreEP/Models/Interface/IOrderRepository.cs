using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public interface IOrderRepository
    {
        IEnumerable<Pedido> Orders { get; }
        void SaveOrder(Pedido order);
    }
}
