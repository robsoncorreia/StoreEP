using StoreEP.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.Repository
{
    public class EFPagmentoRepositorio : IPagamentoRepositorio
    {
        StoreEPDbContext _banco;
        public EFPagmentoRepositorio(StoreEPDbContext storeEPDbContext)
        {
            _banco = storeEPDbContext;
        }
        public IEnumerable<Pedido> Pedidos { get; set; }
        public void SalvarPagamento(Pagamento pagamento)
        {
            if (pagamento.ID == 0)
            {
                _banco.Pagamentos.Add(pagamento);
            }
            else
            {
                Pagamento dbEntry = _banco.Pagamentos.FirstOrDefault(p => p.ID == pagamento.ID);
                if (dbEntry != null)
                {
                    dbEntry.ID = pagamento.ID;
                    dbEntry.UserId = pagamento.UserId;
                    dbEntry.UserId = pagamento.UserId;
                    dbEntry.Valor = pagamento.Valor;
                    dbEntry.CompraDT = pagamento.CompraDT;
                    dbEntry.PagamentoDT = pagamento.PagamentoDT;
                }
            }
            _banco.SaveChanges();
        }
    }
}
