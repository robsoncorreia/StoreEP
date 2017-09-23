using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class EFAddresRepository: IAddressRepositoty
    {
        private StoreEPContext context;
        public EFAddresRepository(StoreEPContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Endereco> Address => context.Address;
        public void SaveAddress(Endereco address)
        {
            if (address.ID == 0)
            {
                context.Address.Add(address);
            }
            else
            {
                Endereco dbEntry = context.Address.FirstOrDefault(p => p.ID == address.ID);
                if (dbEntry != null)
                {
                    dbEntry.UserID = address.UserID;
                    dbEntry.City = address.City;
                    dbEntry.Country = address.Country;
                    dbEntry.GifWrap = address.GifWrap;
                    dbEntry.Line1 = address.Line1;
                    dbEntry.Line2 = address.Line2;
                    dbEntry.Line3 = address.Line3;
                    dbEntry.State = address.State;
                    dbEntry.Zip = address.Zip;
                 }
            }
            context.SaveChanges();
        }
        public Endereco DeleteAddress(int ID)
        {
            Endereco dbEntry = context.Address.FirstOrDefault(a => a.ID == ID);
            if (dbEntry != null)
            {
                context.Address.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
