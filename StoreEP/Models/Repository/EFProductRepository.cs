using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class EFProductRepository: IProductRepository
    {
        private StoreEPContext context;
        public EFProductRepository(StoreEPContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Produto> Produtos => context.Produto;
        public void SaveProduct(Produto produto)
        {
            if (produto.ProdutoID == 0) {
                context.Produto.Add(produto);
            }
            else
            {
                Produto dbEntry = context.Produto.FirstOrDefault(p => p.ProdutoID == produto.ProdutoID);
                if(dbEntry != null)
                {
                    dbEntry.NomePD = produto.NomePD;
                    dbEntry.DescricaoPD = produto.DescricaoPD;
                    dbEntry.PrecoPD = produto.PrecoPD;
                    dbEntry.CategoriaPD = produto.CategoriaPD;
                }
            }
            context.SaveChanges();
        }
        public Produto DeleteProduto(int produtoId)
        {
            Produto dbEntry = context.Produto.FirstOrDefault(p => p.ProdutoID == produtoId);
            if (dbEntry != null)
            {
                context.Produto.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
