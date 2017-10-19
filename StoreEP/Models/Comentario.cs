using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class Comentario
    {
        public int ID { get; set; }
        public string Assunto { get; set; }
        public byte Estrela { get; set; }
        public string Texto { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime Data { get; set; }
        public string UsuarioID { get; set; }
        public ICollection<Comentario> Respostas { get; set; }
        public bool Aprovado { get; set; } = false;
    }
}
