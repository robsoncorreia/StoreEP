using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class EFProdutoRepositorio : IProdutoRepositorio
    {
        private StoreEPDbContext _bancoContexto;
        private int id = 0;
        public EFProdutoRepositorio(StoreEPDbContext ctx)
        {
            _bancoContexto = ctx;
        }
        public IEnumerable<Produto> Produtos => _bancoContexto.Produtos.Include(i => i.Imagens).Include(c => c.Comentarios);
        public int RegistrarProduto(Produto produto)
        {
            if (produto.ProdutoID == 0)
            {
                _bancoContexto.Produtos.Add(produto);
                id = produto.ProdutoID;
            }
            else
            {
                Produto dbEntry = _bancoContexto.Produtos.FirstOrDefault(p => p.ProdutoID == produto.ProdutoID);
                if (dbEntry != null)
                {
                    dbEntry.ProdutoID = produto.ProdutoID;
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
            return id;
        }
        public Produto ApagarProduto(int ID)
        {
            Produto dbEntry = _bancoContexto.Produtos.FirstOrDefault(p => p.ProdutoID == ID);
            if (dbEntry != null)
            {
                _bancoContexto.Produtos.Remove(dbEntry);
                _bancoContexto.SaveChanges();
            }
            return dbEntry;
        }
    }
}
