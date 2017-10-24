using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.ViewModels
{
    public class CartaoProdutoViewModel
    {
        public IEnumerable<Imagem> Imagens { get; set; }
        public Produto Produto { get; set; }
    }
}
