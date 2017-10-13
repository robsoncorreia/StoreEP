using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public interface IProdutoRepositorio 
    {
        IEnumerable<Produto> Produtos { get; }
        int RegistrarProduto(Produto produto);
        Produto ApagarProduto(int ID);
    }
}
