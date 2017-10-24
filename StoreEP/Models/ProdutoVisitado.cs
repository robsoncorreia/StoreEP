using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class ProdutoVisitado
    {
        public int ProdutoVisitadoID { get; set; }
        public Produto Produto { get; set; }
        public DateTime DataHoraVisita { get; set; } = DateTime.Now;
        public int QuantidadeVisita { get; set; } = 1;
        public string UserID { get; set; }
    }
}
