using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.ViewModels
{
    public class AddressViewModel
    {
        public IEnumerable<Address> Address { get; set; }
        public string UserId { get; set; }
    }
}
