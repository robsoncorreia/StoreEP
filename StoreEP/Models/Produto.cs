using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class Produto
    {
        public int ProdutoID { get; set; }
        [Required(ErrorMessage = "Escreva um nome válido.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Defina a categoria.")]
        public string Categoria { get; set; }
        [Required(ErrorMessage = "Estipule um preço.")]
        public decimal Preco { get; set; }
        [Required(ErrorMessage = "Escreva um descrição sobre o produto.")]
        public string Descricao { get; set; }
        //[Required(ErrorMessage = "Digite o link da imagem.")]
        public virtual ICollection<Imagem> Imagens { get; set; }
        //[Required(ErrorMessage = "Quem é o fabricante.")]
        public string Fabricante { get; set; }
        public virtual ICollection<Avaliacao> Avaliacoes { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public int Quantidade { get; set; } 
        public bool Publicado { get; set; } = false;
        public float Peso { get; set; }
    }
}
