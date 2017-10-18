using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StoreEP.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }
        public string UserId { get; set; }
        public bool Shipped { get; set; } = false;    
        public ICollection<CartLine> Lines { get; set; }
        public Endereco Endereco { get; set; }
        public Pagamento Pagamento { get; set; }
        public DateTime DataCompra { get; set; }
    }
}
