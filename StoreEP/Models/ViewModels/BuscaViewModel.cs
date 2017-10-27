using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.ViewModels
{
    public class BuscaViewModel
    {
        [Required]
        public string Texto { get; set; }
        public DateTime DataBusca { get; set; } = DateTime.Now;
    }
}
