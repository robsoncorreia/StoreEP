using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.ViewModels
{
    public class EnderecoViewModels
    {
        public IEnumerable<Endereco> Enderecos { get; set; }
        public int EnderecoID { get; set; }
    }
}
