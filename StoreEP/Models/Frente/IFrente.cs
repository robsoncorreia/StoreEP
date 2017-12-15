using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.Frente
{
    public interface IFrente
    {
        decimal Calcular(Pedido pedido);
    }
}
