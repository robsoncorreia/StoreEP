using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.OrderViewModel
{
    public class ListModel
    {
        public IEnumerable<Order> GetOrder { get; set; }
        public IEnumerable<Address> GetAddress { get; set; }
    }
}
