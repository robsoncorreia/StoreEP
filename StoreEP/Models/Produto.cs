using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoID { get; set; }
        public string NomePD { get; set; }
        public string CategoriaPD { get; set; }
        public decimal PrecoPD { get; set; }
        public string DescricaoPD { get; set; }
        public Produto RelacionadoPD { get; set; }
        public string LinkImagemPD { get; set; }
    }
}
