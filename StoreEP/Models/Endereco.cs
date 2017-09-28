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
        public string Rua { get; set; }
        [Required(ErrorMessage = "Entre com o nome bairro.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$", ErrorMessage = "Apenas caractéres.")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Entre com o número.")]
        [RegularExpression(@"^[0-9]{1,100}$", ErrorMessage = "Apenas números.")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "Por favor digite o nome da cidade.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$", ErrorMessage = "Apenas caractéres.")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Por favor digite o nome do estado.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$", ErrorMessage = "Apenas caractéres.")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "Por favor digite o Cep.")]
        public string CEP { get; set; }
        [Required(ErrorMessage = "Digite o nome do pais.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$", ErrorMessage = "Apenas caractéres.")]
        public string Pais { get; set; }
        public bool GifWrap { get; set; }
    }
}