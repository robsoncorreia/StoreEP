using System.Collections.Generic;
namespace StoreEP.Models
{
    public interface IProdutoRepositorio{
        IEnumerable<Produto> Produtos  {get;}
    }
}