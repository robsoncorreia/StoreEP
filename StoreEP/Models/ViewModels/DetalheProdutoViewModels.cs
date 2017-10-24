using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.ViewModels
{
    public class DetalheProdutoViewModels
    {
        public IEnumerable<Imagem> Imagens { get; set; }
        public Produto Produto { get; set; }
        public Comentario Comentario { get; set; }
        public Comentario Resposta { get; set; }
        public IEnumerable<Comentario> Comentarios { get; set; }
        public IEnumerable<HistoricoPreco> HistoricosPreco { get; set; }

        
    }
}
