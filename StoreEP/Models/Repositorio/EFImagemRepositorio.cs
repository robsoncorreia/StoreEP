using StoreEP.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Models.Repositorio
{
    public class EFImagemRepositorio: IImagensRepositorio
    {
        private StoreEPDbContext _bancoContexto;
        public EFImagemRepositorio(StoreEPDbContext  storeEPDbContext)
        {
            _bancoContexto = storeEPDbContext;
        }
        public IEnumerable<Imagem> Imagens => _bancoContexto.Imagens;
        public void RegistrarImagem(Imagem imagem)
        {
            if (imagem.ID == 0)
            {
                _bancoContexto.Imagens.Add(imagem);
            }
            else
            {
                Imagem dbEntry = _bancoContexto.Imagens.FirstOrDefault(i => i.ID == imagem.ID);
                if (dbEntry != null)
                {
                    dbEntry.Nome = imagem.Nome;
                    dbEntry.Link = imagem.Link;
                    dbEntry.ID = imagem.ID;
                }
            }
            _bancoContexto.SaveChanges();
        }
        public Imagem ApagarImagem(int id)
        {
            Imagem dbEntry = _bancoContexto.Imagens.FirstOrDefault(i => i.ID == id);
            if (dbEntry != null)
            {
                _bancoContexto.Imagens.Remove(dbEntry);
                _bancoContexto.SaveChanges();
            }
            return dbEntry;
        }
    }
}
