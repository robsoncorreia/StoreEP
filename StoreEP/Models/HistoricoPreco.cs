using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class HistoricoPreco
    {
        [Key]
        public int PrecoId { get; set; }
        public decimal PrecoAntigo { get; set; }
        public decimal PrecoNovo { get; set; }
        public DateTime DataAltarecao { get; set; } 
        public int ID { get; set; }
    }
}
