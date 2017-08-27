using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreEP.Models;

namespace StoreEP.Models
{
    public class SimpleReposity
    {
        private static SimpleReposity sharedRepository = new SimpleReposity();
        private Dictionary<string, Produto> produtos = new Dictionary<string, Produto>();
        public static SimpleReposity SharedRepository => sharedRepository;
        public void AddProduto(Produto p) => produtos.Add(p.NM_Produto, p);
        public IEnumerable<Produto> Produtos => produtos.Values;
        public SimpleReposity()
        {
            var initialItens = new[] {
                new Produto {
                    NM_Produto = "Camera",
                    CD_Produto = 1,
                    Preco = 10M,
                    Imagem = "http://imgsv.imaging.nikon.com/lineup/dslr/d5/img/Produto_01_01.png",
                    PD_Fabricante = "Nikon",
                    NM_Categoria = "Fotografia"

                },
                new Produto {
                    NM_Produto = "SSD",
                    CD_Produto = 2,
                    Preco = 20M,
                    PD_Fabricante = "KingStone",

                    Imagem =  "http://images.tcdn.com.br/img/img_prod/49613/kingston_ssd_2_5_120gb_v300_sata_iii_sv300s37a_120g_2911_1_20150406164044.jpg",
                    NM_Categoria = "Informática"

                },
                new Produto {
                    NM_Produto = "Tv",
                    CD_Produto = 3,
                    Preco = 30M,
                    Imagem = "https://www.hirschs.co.za/media/catalog/Produto/cache/1/thumbnail/600x/17f82f742ffe127f42dca9de82fb58b1/5/6/56409.jpg",
                    PD_Fabricante = "Samsung",
                    NM_Categoria = "Eletro Eletrônicos"
                }
            };
            foreach (var p in initialItens)
            {
                AddProduto(p);
            }
        }
    }
}