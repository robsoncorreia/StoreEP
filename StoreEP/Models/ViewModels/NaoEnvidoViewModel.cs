using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.ViewModels
{
    public class NaoEnvidoViewModel
    {
        public IEnumerable<Pedido> Pedidos { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
