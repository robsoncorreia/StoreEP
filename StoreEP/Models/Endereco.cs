using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class Endereco
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        [Required(ErrorMessage = "Entre com o endereço.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$", ErrorMessage = "Apenas caractéres.")]
        [Display(Name = "Rua")]
        public string Line1 { get; set; }
        [Required(ErrorMessage = "Entre com o nome bairro.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$", ErrorMessage = "Apenas caractéres.")]
        public string Line2 { get; set; }
        [Required(ErrorMessage = "Entre com o número.")]
        [RegularExpression(@"^[0-9]{1,100}$", ErrorMessage = "Apenas números.")]
        public string Line3 { get; set; }
        [Required(ErrorMessage = "Por favor digite o nome da cidade.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$", ErrorMessage = "Apenas caractéres.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Por favor digite o nome do estado.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$", ErrorMessage = "Apenas caractéres.")]
        public string State { get; set; }
        [Required(ErrorMessage = "Por favor digite o Cep.")]
        public string Zip { get; set; }
        [Required(ErrorMessage = "Digite o nome do pais.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$", ErrorMessage = "Apenas caractéres.")]
        public string Country { get; set; }
        public bool GifWrap { get; set; }
    }
}
