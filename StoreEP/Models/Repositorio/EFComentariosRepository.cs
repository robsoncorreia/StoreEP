using StoreEP.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.Repositorio
{
    public class EFComentariosRepository : IComentariosRepositorio
    {
        private StoreEPDbContext _context;

        public EFComentariosRepository(StoreEPDbContext ctx)
        {
            _context = ctx;
        }
        public IEnumerable<Comentario> Comentarios => _context.Comentarios;
        public void RegistrarComentario(Comentario comentario)
        {
            if (comentario.ComentarioID == 0)
            {
                _context.Comentarios.Add(comentario);
            }
            else
            {
                Comentario dbEntry = _context.Comentarios.FirstOrDefault(c => c.ComentarioID == comentario.ComentarioID);
                if (dbEntry != null)
                {
                    _context.Comentarios.Update(comentario);
                }
            }
            _context.SaveChanges();
        }
        public int ApagarComentario(Comentario comentario)
        {
            _context.Remove(comentario);
            return _context.SaveChanges();
        }
        public int RegistrarResposta(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            return _context.SaveChanges();
        }
    }
}


