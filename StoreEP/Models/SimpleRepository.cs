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
                    ImagemProduto = "https://images-submarino.b2w.io/produtos/01/00/sku/9266/0/9266029_1GG.jpg",
                    PD_Fabricante = "Nikon",
                    NM_Categoria = "Fotografia"
                },
                new Produto {
                    NM_Produto = "SSD",
                    CD_Produto = 2,
                    Preco = 20M,
                    PD_Fabricante = "KingStone",
                    ImagemProduto =  "http://images.tcdn.com.br/img/img_prod/49613/kingston_ssd_2_5_120gb_v300_sata_iii_sv300s37a_120g_2911_1_20150406164044.jpg",
                    NM_Categoria = "Informática"

                },
                new Produto {
                    NM_Produto = "Tv",
                    CD_Produto = 3,
                    Preco = 30M,
                    ImagemProduto = "https://images-submarino.b2w.io/produtos/01/00/item/132380/7/132380720_1GG.jpg",
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