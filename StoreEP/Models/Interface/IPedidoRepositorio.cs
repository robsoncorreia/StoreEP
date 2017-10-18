using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public interface IPedidoRepositorio
    {
        IEnumerable<Pedido> Pedidos { get; }
        void Registrar(Pedido pedido);
    }
}
