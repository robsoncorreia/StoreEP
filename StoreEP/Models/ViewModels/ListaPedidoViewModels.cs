using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.ViewModels
{
    public class ListaPedidoViewModels
    {
        public IEnumerable<Pedido> Pedidos { get; set; }
        public IEnumerable<Endereco> Enderecos { get; set; }
    }
}
