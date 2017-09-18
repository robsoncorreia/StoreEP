using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StoreEP.Models
{
    public class Order
    {
        public string UserID { get; set; }
        public bool Shipped { get; set; } = false;
        public int OrderID { get; set; }
        public ICollection<CartLine> Lines { get; set; }
        public Address Address { get; set; }
    }
}
