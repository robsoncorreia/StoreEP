using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.ViewModels
{
    public class DetalheProdutoViewModels
    {
        public Produto Produto { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public Avaliacao Resposta { get; set; }
        public IEnumerable<HistoricoPreco> HistoricosPreco { get; set; }     
    }
}
