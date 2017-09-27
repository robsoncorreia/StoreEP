using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.ViewModels
{
    public class FinalizarPedidoViewModel
    {
        public IEnumerable<Endereco> Enderecos { get; set; }
        public Carrinho Carrinho { get; set; }

    }
}
