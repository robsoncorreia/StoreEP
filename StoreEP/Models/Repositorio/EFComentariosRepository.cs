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
            if (comentario.ComentarioId == 0)
            {
                _context.Comentarios.Add(comentario);
            }
            else
            {
                Comentario dbEntry = _context.Comentarios.FirstOrDefault(c => c.ComentarioId == comentario.ComentarioId);
                if (dbEntry != null)
                {
                    dbEntry.UsuarioID = comentario.UsuarioID;
                    dbEntry.Data = comentario.Data;
                    dbEntry.Estrela = comentario.Estrela;
                    dbEntry.NomeUsuario = comentario.NomeUsuario;
                    dbEntry.ProdutoId = comentario.ProdutoId;
                    dbEntry.Respostas = comentario.Respostas;
                    dbEntry.Texto = comentario.Texto;
                }
            }
            _context.SaveChanges();
        } 
        
    }
}


