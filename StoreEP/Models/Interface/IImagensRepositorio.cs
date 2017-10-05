using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.Interface
{
    public interface IImagensRepositorio
    {
        void RegistrarImagem(Imagem imagem);
        IEnumerable<Imagem> Imagens { get; }
        Imagem ApagarImagem(int id);
    }
}
