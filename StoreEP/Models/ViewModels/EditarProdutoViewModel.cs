using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.ViewModels
{
    public class EditarProdutoViewModel
    {
        public Produto Produto { get; set; }
        public IEnumerable<string> Categorias { get; set; }
        public Imagem Imagem { get; set; }
        public IEnumerable<string> Fabricantes { get; set; }
    }
}
