using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class EFProdutoRepositorio: IProdutoRepositorio
    {
        private StoreEPDbContext context;
        public EFProdutoRepositorio(StoreEPDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Produto> Produtos => context.Produtos;
        public void RegistrarProduto(Produto produto)
        {
            if (produto.ProdutoID == 0) {
                context.Produtos.Add(produto);
            }
            else
            {
                Produto dbEntry = context.Produtos.FirstOrDefault(p => p.ProdutoID == produto.ProdutoID);
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
        public Produto ApagarProduto(int produtoId)
        {
            Produto dbEntry = context.Produtos.FirstOrDefault(p => p.ProdutoID == produtoId);
            if (dbEntry != null)
            {
                context.Produtos.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
