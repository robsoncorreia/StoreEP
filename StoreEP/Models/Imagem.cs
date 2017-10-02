using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class Imagem
    {
        [Key]
        public int ImagemId { get; set; }
        public int ProdutoId { get; set; }
        public string Link { get; set; }
        public string Nome { get; set; }
    }
}
