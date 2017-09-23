using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.OrderViewModel
{
    public class ListModel
    {
        public IEnumerable<Pedido> GetOrder { get; set; }
        public IEnumerable<Endereco> GetAddress { get; set; }
    }
}
