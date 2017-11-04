using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models
{
    public class Avaliacao
    {
        public int AvaliacaoID { get; set; }
        public int ProdutoID { get; set; }
        [Required]
        public string Titulo { get; set; }
        public byte Estrela { get; set; }
        [Required]
        public string Texto { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public string UsuarioID { get; set; }
        public ICollection<Avaliacao> Respostas { get; set; }
        public bool Aprovado { get; set; } = false;
    }
}
