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
        public int PedidoID { get; set; }
        public string UserId { get; set; }
        public bool Enviado { get; set; } = false;    
        public virtual ICollection<CartLine> Lines { get; set; }
        public  Endereco Endereco { get; set; }
        public  Pagamento Pagamento { get; set; }
        public DateTime DataCompra { get; set; } = DateTime.Now;
    }
}
