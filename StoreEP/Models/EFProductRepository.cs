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
    }
}
