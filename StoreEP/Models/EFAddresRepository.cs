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
        public IEnumerable<Address> Address => context.Address;
        public void SaveAddress(Address address)
        {
            if (address.AddressID == 0)
            {
                context.Address.Add(address);
            }
            else
            {
                Address dbEntry = context.Address.FirstOrDefault(p => p.AddressID == address.AddressID);
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
        public Address DeleteAddress(int addressId)
        {
            Address dbEntry = context.Address.FirstOrDefault(a => a.AddressID == addressId);
            if (dbEntry != null)
            {
                context.Address.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
