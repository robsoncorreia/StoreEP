using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.ViewModels
{
    public class FinalizarPedidoViewModel
    {
        public Endereco Endereco { get; set; }
        public Carrinho Carrinho { get; set; }
        public Pagamento Pagamento { get; set; }
        public int EnderecoId { get; set; }
    }
}
