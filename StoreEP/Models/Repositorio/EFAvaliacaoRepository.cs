using Microsoft.EntityFrameworkCore;
using StoreEP.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.Repositorio
{
    public class EFAvaliacaoRepository : IAvaliacaoRepositorio
    {
        private StoreEPDbContext _context;

        public EFAvaliacaoRepository(StoreEPDbContext ctx)
        {
            _context = ctx;
        }
        public IEnumerable<Avaliacao> Avaliacoes => _context.Avaliacoes
                                                            .Include(a => a.Respostas)
                                                            .Where(a => a.Aprovado == true);
        public void RegistrarComentario(Avaliacao avaliacao)
        {
            if (avaliacao.AvaliacaoID == 0)
            {
                _context.Avaliacoes.Add(avaliacao);
            }
            else
            {
                Avaliacao dbEntry = _context.Avaliacoes
                                            .FirstOrDefault(c => c.AvaliacaoID == avaliacao.AvaliacaoID);
                if (dbEntry != null)
                {
                    _context.Avaliacoes.Update(avaliacao);
                }
            }
            _context.SaveChanges();
        }
        public int ApagarComentario(Avaliacao avaliacao)
        {
            _context.Remove(avaliacao);
            return _context.SaveChanges();
        }
        public int RegistrarResposta(Avaliacao avaliacao, Avaliacao resposta)
        {
            avaliacao.Respostas.Add(resposta);
            return _context.SaveChanges();
        }
    }
}


