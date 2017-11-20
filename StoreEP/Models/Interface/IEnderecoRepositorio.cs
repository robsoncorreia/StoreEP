using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public interface IEnderecoRepositorio
    {
        IEnumerable<Endereco> Enderecos { get; }
        int SalvarEndereco(Endereco endereco);
        Endereco ApagarEndereco(int id);
    }
}
