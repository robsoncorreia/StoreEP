using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class Imagem
    {
        [Key]
        public int ImagemID { get; set; }
        public int ProdutoID { get; set; }
        [Url]
        [Required]
        public string Link { get; set; }
        [Required]
        public string Nome { get; set; }
    }
}
