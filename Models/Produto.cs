using StoreEP.Models;

namespace StoreEP
{
    public class Produto
    {
        public int CD_Produto { get; set; }
        public string NM_Produto { get; set; }
        public string NM_Categoria { get; set; }
        public decimal Preco { get; set; }
        public Produto PD_Relacionado {get;set;}
        public bool EmEstoque {get;} = true;
        public string PD_Fabricante { get; set; }
        public string PD_Descricao { get; set; }
        private string imagem;
        public string Imagem
        {
            get { return imagem;}
            set { imagem = value??"oi";}
        }
    

        public static Produto[] GetProduto(){
            var lista = new[]{
                new Produto{
                    NM_Produto = "Kayak",
                    Preco = 275M,
                    NM_Categoria = "Esportes aquaticos"
                },
                new Produto{
                    NM_Produto = "Lifejacket",
                    Preco = 48.95M,
                    NM_Categoria = "Esportes aquaticos"
                }
            };
            return lista;
        }
    }
}