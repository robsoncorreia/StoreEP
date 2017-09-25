using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class Comentario
    {
        public int ID { get; set; }
        public int ProdutoID { get; set; }
        public byte Estrela { get; set; }
        public string Texto { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime Data { get; set; }
    }
}
