using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.Interface
{
    public interface IComentariosRepositorio
    {
        void RegistrarComentario(Comentario comentario);
        IEnumerable<Comentario> Comentarios { get; }
        int ApagarComentario(Comentario comentario);
    }
}
