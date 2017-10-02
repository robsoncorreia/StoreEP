using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class Pagamento
    {
        [Key]
        public int PagamentoId { get; set; }
        public string UserId { get; set; }
        public int PedidoId { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public DateTime CompraDT { get; set; }
        public DateTime? PagamentoDT { get; set; } = null;
    }
}
