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
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "Escreva um nome válido.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Apenas caractéres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Defina a categoria.")]
        public string Categoria { get; set; }
        [Required(ErrorMessage = "Estipule um preço.")]
        public decimal Preco { get; set; }
        [Required(ErrorMessage = "Escreva um descrição sobre o produto.")]
        public string Descricao { get; set; }
        //[Required(ErrorMessage = "Digite o link da imagem.")]
        public List<Imagem> Imagens { get; set; }
        //[Required(ErrorMessage = "Quem é o fabricante.")]
        public string Fabricante { get; set; }
        public List<Comentario> Comentarios { get; set; } 
    }
}
