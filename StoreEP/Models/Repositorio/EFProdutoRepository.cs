using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class EFProdutoRepositorio: IProdutoRepositorio
    {
        private StoreEPDbContext _bancoContexto;
        public EFProdutoRepositorio(StoreEPDbContext ctx)
        {
            _bancoContexto = ctx;
        }
        public IEnumerable<Produto> Produtos => _bancoContexto.Produtos;
        public void RegistrarProduto(Produto produto)
        {
            if (produto.ProdutoId == 0) {
                _bancoContexto.Produtos.Add(produto);
            }
            else
            {
                Produto dbEntry = _bancoContexto.Produtos.FirstOrDefault(p => p.ProdutoId == produto.ProdutoId);
                if(dbEntry != null)
                {
                    dbEntry.Nome = produto.Nome;
                    dbEntry.Descricao = produto.Descricao;
                    dbEntry.Preco = produto.Preco;
                    dbEntry.Categoria = produto.Categoria;
                }
            }
            _bancoContexto.SaveChanges();
        }
        public Produto ApagarProduto(int ID)
        {
            Produto dbEntry = _bancoContexto.Produtos.FirstOrDefault(p => p.ProdutoId == ID);
            if (dbEntry != null)
            {
                _bancoContexto.Produtos.Remove(dbEntry);
                _bancoContexto.SaveChanges();
            }
            return dbEntry;
        }
    }
}
