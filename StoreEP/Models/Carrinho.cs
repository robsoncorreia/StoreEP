using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class Carrinho
    {
        private static Carrinho instancia;

        public static Carrinho Instance
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Carrinho();
                }
                return instancia;
            }
        }
        private List<CartLine> lineCollection = new List<CartLine>();
        public virtual void AddItem(Produto produto, int quantidade = 1)
        {
            CartLine line = lineCollection.Where(p => p.Produto.ProdutoID == produto.ProdutoID)
                                            .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Produto = produto,
                    Quantidade = quantidade
                });
            }
            else
            {
                line.Quantidade += quantidade;
            }
        }
        public virtual void RemoveLine(Produto produto) => lineCollection.RemoveAll(l => l.Produto.ProdutoID == produto.ProdutoID);
        public virtual decimal ValorTotal() => lineCollection.Sum(e => e.Produto.Preco * e.Quantidade);
        public virtual int QuantidadeTotal() => lineCollection.Sum(e => e.Quantidade);
        public virtual void Limpar() => lineCollection.Clear();
        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }
    public class CartLine
    {
        public int CartLineID { get; set; }
        public virtual Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
