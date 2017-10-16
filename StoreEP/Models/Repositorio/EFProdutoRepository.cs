using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class EFProdutoRepositorio : IProdutoRepositorio
    {
        private StoreEPDbContext _bancoContexto;
        private int produtoid = 0;
        public EFProdutoRepositorio(StoreEPDbContext ctx)
        {
            _bancoContexto = ctx;
        }
        public IEnumerable<Produto> Produtos => _bancoContexto.Produtos;
        public int RegistrarProduto(Produto produto)
        {
            if (produto.ProdutoId == 0)
            {
                _bancoContexto.Produtos.Add(produto);
                produtoid = produto.ProdutoId;
            }
            else
            {
                Produto dbEntry = _bancoContexto.Produtos.FirstOrDefault(p => p.ProdutoId == produto.ProdutoId);
                if (dbEntry != null)
                {
                    produtoid = produto.ProdutoId;
                    dbEntry.Categoria = produto.Categoria;
                    dbEntry.Descricao = produto.Descricao;
                    dbEntry.Fabricante = produto.Fabricante;
                    dbEntry.Nome = produto.Nome;
                    dbEntry.Preco = produto.Preco;
                    dbEntry.Publicado = produto.Publicado;
                    dbEntry.Quantidade = dbEntry.Quantidade;
                }
            }
            _bancoContexto.SaveChanges();
            return produtoid;
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
