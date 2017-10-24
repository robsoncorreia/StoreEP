using StoreEP.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.Repositorio
{
    public class EFHistoricoPreco : IHistoricoPrecosRepositorio
    {
        private StoreEPDbContext _context;

        public EFHistoricoPreco(StoreEPDbContext ctx)
        {
            _context = ctx;
        }
        public IEnumerable<HistoricoPreco> HistoricosPreco => _context.HistoricoPrecos;
    }
}


