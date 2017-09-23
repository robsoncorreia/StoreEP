using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public interface IAddressRepositoty
    {
        IEnumerable<Endereco> Address { get; }
        void SaveAddress(Endereco address);
    }
}
