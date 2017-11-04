using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.Interface
{
    public interface IAvaliacaoRepositorio
    {
        void RegistrarComentario(Avaliacao avaliacao);
        IEnumerable<Avaliacao> Avaliacoes { get; }
        int ApagarComentario(Avaliacao avaliacao);
        int RegistrarResposta(Avaliacao avaliacao, Avaliacao resposta);
    }
}
