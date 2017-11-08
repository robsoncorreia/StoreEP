using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class Endereco
    {
        public int EnderecoID { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage = "Entre com o endereço.")]
        public string Rua { get; set; }
        [Required(ErrorMessage = "Entre com o nome bairro.")]
        public string Bairro { get; set; }
        [Display(Name = "Número")]
        [Required(ErrorMessage = "Entre com o número.")]
        [RegularExpression(@"^[0-9]{1,100}$", ErrorMessage = "Apenas números.")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "Por favor digite o nome da cidade.")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Por favor digite o nome do estado.")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "Por favor digite o Cep.")]
        public string CEP { get; set; }
        [Required(ErrorMessage = "Digite o nome do pais.")]
        [Display(Name = "País")]
        public string Pais { get; set; }
        public string Complemento { get; set; }
        public DateTime DataUtilizacao { get; set; } = DateTime.Now;
    }
}
