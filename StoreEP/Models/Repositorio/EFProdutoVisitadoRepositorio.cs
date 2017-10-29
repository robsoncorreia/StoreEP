using Microsoft.EntityFrameworkCore;
using StoreEP.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StoreEP.Models.Repositorio
{
    public class EFProdutoVisitadoRepositorio : IProdutoVisitadoRepositorio
    {
        private StoreEPDbContext _context;

        public EFProdutoVisitadoRepositorio(StoreEPDbContext ctx)
        {
            _context = ctx;
        }
        public IEnumerable<ProdutoVisitado> ProdutosVisitados => _context.ProdutosVisitados
                                                                            .Include(p => p.Produto)   
                                                                            .Include(i => i.Produto.Imagens);
        public int AdicionarProdutoVisitado(string userID, Produto produto)
        {
            ProdutoVisitado produtoVisitado = _context.ProdutosVisitados
                                                        .Where(p => p.Produto.ProdutoID == produto.ProdutoID && p.UserID.Equals(userID))
                                                        .FirstOrDefault();
            if (produtoVisitado == null && userID != null)
            {
                _context.ProdutosVisitados.Add(new ProdutoVisitado {
                    UserID =userID,
                    Produto = produto
                });
            }
            else if (produtoVisitado != null && userID != null)
            {
                produtoVisitado.QuantidadeVisita += 1;
                _context.ProdutosVisitados.Update(produtoVisitado);
            }
            return _context.SaveChanges();
        }
    }
}


