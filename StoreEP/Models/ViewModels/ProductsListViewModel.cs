using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Produto> Produtos { get; set; }
        public IEnumerable<Imagem> Imagens { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
