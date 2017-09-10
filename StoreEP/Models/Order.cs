using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StoreEP.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }
        [Required(ErrorMessage = "Por favor entre com um nome.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Entre com o primeiro endereço.")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        [Required(ErrorMessage = "Por favor digite o nome da cidade.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Por favor digite o nome do estado.")]
        public string State { get; set; }
        public string Zip { get; set; }
        [Required(ErrorMessage = "Digie o nome do pais.")]
        public string Country { get; set; }
        public bool GifWrap { get; set; }
    }
}
