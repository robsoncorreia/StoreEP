using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.Frente
{
    public class Transportadora : IFrente
    {
        public decimal Calcular(Pedido pedido)
        {
            return (decimal)pedido.Lines.Sum(x => x.Produto.Peso) * 1.4m;
        }
    }
}
