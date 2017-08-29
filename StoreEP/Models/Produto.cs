using StoreEP.Models;
using System.ComponentModel.DataAnnotations;

namespace StoreEP
{
    public class Produto
    {
        [Key]
        public int CD_Produto { get; set; }
        public string NM_Produto { get; set; }
        public string NM_Categoria { get; set; }
        public decimal Preco { get; set; }
        public Produto PD_Relacionado {get;set;}
        public bool EmEstoque {get;} = true;
        public string PD_Fabricante { get; set; }
        public string PD_Descricao { get; set; }
        public string ImagemProduto { get; set; }
    
    }
}