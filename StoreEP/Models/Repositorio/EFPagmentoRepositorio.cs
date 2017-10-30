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
            if (pagamento.PagamentoID == 0)
            {
                _banco.Pagamentos.Add(pagamento);
            }
            else
            {
                Pagamento dbEntry = _banco.Pagamentos.FirstOrDefault(p => p.PagamentoID == pagamento.PagamentoID);
                if (dbEntry != null)
                {
                    dbEntry.PagamentoID = pagamento.PagamentoID;
                    dbEntry.UserId = pagamento.UserId;
                    dbEntry.UserId = pagamento.UserId;
                    dbEntry.Valor = pagamento.Valor;
                    dbEntry.DataCompra = pagamento.DataCompra;
                    dbEntry.DataPagamento = pagamento.DataPagamento;
                }
            }
            _banco.SaveChanges();
        }
    }
}
