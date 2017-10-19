using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class Imagem
    {
 
        public int ID { get; set; }
        [Url]
        [Required]
        public string Link { get; set; }
        [Required]
        public string Nome { get; set; }
    }
}
