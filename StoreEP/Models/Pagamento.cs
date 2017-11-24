using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public enum FormaPagamento : byte { CartaoCredito = 1, Boleto = 2, Dinheiro = 3 };
    public class Pagamento
    {
        [Key]
        public int PagamentoID { get; set; }
        public string UserId { get; set; }
        public int PedidoId { get; set; }
        [Required]
        public decimal Valor { get; set; }
        public DateTime DataCompra { get; set; } = DateTime.Now;
        public DateTime DataPagamento { get; set; }
        public byte TipoPagamento { get; set; }
    }
}
