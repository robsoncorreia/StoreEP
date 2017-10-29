using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.Interface
{
    public interface IProdutoVisitadoRepositorio
    {
        int AdicionarProdutoVisitado(string userID, Produto produto);
        IEnumerable<ProdutoVisitado> ProdutosVisitados { get; }
    }
}
