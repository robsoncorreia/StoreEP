using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.ViewModels
{
    public class AdminIndexViewModel
    {
        public int ComentariosNaoAprovados { get; set; }
        public int NumeroProdutosRegistrados { get; set; }
        public int PedidosNaoEnviados { get; set; }
        public string Selecionado { get; set; }
    }
}
