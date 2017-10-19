using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class EFEnderecoRepositorio : IEnderecoRepositorio
    {
        private StoreEPDbContext context;
        public EFEnderecoRepositorio(StoreEPDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Endereco> Enderecos => context.Enderecos;

        public void SalvarEndereco(Endereco endereco)
        {
            if (endereco.ID == 0)
            {
                context.Enderecos.Add(endereco);
            }
            else
            {
                Endereco dbEntry = context.Enderecos.FirstOrDefault(p => p.ID == endereco.ID);
                if (dbEntry != null)
                {
                    dbEntry.UserId = endereco.UserId;
                    dbEntry.Cidade = endereco.Cidade;
                    dbEntry.Pais = endereco.Pais;
                    dbEntry.GifWrap = endereco.GifWrap;
                    dbEntry.Rua = endereco.Rua;
                    dbEntry.Bairro = endereco.Bairro;
                    dbEntry.Numero = endereco.Numero;
                    dbEntry.Estado = endereco.Estado;
                    dbEntry.CEP = endereco.CEP;
                }
            }
            context.SaveChanges();
        }
        public Endereco ApagarEndereco(int id)
        {
            Endereco dbEntry = context.Enderecos.FirstOrDefault(a => a.ID == id);
            if (dbEntry != null)
            {
                dbEntry.UserId = "apagado";
                context.Enderecos.Update(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
