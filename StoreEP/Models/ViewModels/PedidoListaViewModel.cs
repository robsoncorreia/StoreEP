using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.ViewModels
{
    public class PedidoListaViewModel
    {
        public IEnumerable<Pedido> Pedidos { get; set; }
    }
}
