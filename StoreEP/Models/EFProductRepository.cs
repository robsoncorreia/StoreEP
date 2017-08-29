using System.Collections.Generic;
namespace StoreEP.Models
{
    public class EFProductRepository : IProdutoRepositorio{
       private ApplicationDbContext context;
        public EFProductRepository(ApplicationDbContext ctx){
            context = ctx;
        }
        public IEnumerable<Produto> Produtos => context.Produtos;
    }
}