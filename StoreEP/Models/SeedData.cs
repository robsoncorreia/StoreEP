using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StoreEP.Models
{
    public static class SeedData
    {

        public static void EnsurePopulated(StoreEPDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Produtos.Any())
            {
                return;   // DB has been seeded
            }
            var produtos = new Produto[] {
                    new Produto
                    {
                        Nome = "Aparador de Pelos",
                        Descricao = "Kit Aparador de Pelos Philips Multigroom QG3339/15 Bateria Recarregável 12W",
                        Categoria = "Beleza & Perfumaria",
                        Preco = 275m  ,
                        Fabricante ="Philips",
                        Imagens =  new List<Imagem>{
                            new Imagem {
                                Link = "https://images-submarino.b2w.io/produtos/01/00/item/119371/0/119371001_1GG.png",
                                Nome = "Aparador de Pelos 1"
                            },
                            new Imagem {
                                Link = "https://images-submarino.b2w.io/produtos/01/00/item/119371/0/119371001_2GG.jpg",
                                Nome = "Aparador de Pelos 2"
                            },
                            new Imagem
                            {
                                Link = "https://images-submarino.b2w.io/produtos/01/00/item/119371/0/119371001_3GG.jpg",
                                Nome = "Aparador de Pelos 3"
                            }
                        }
                    },
                    new Produto
                    {
                        Nome = "HD Externo Portátil WD Elements 1TB USB 3.0",
                        Descricao = "O HD Externo Portátil WD faz transferências de dados ultrarrápidas e possui 1TB de capacidade.",
                        Categoria = "Acessórios de Informática",
                        Preco = 254.9m,
                        Fabricante = "Wester Digital",
                        Imagens =  new List<Imagem>{
                            new Imagem {
                                Link = "https://images-submarino.b2w.io/produtos/01/00/item/123403/3/123403301_1GG.png",
                                Nome = "HD Externo 1"
                            },
                            new Imagem {
                                Link = "https://images-submarino.b2w.io/produtos/01/00/item/123403/3/123403301_2GG.jpg",
                                Nome = "HD Externo 2"
                            },
                            new Imagem
                            {
                                Link = "https://images-submarino.b2w.io/produtos/01/00/item/123403/3/123403301_3GG.jpg",
                                Nome = "HD Externo 3"
                            }
                        }
                    },
                    new Produto
                    {
                        Nome = "Notebook Dell Inspiron i15-7560-A30S Intel Core i7 16GB",
                        Descricao = "Notebook Dell Inspiron i15-7560-A30S Intel Core i7 16GB ((GeForce 940MX de 4GB)) 1TB 128GB SSD Tela Full HD 15,6 Windows 10",
                        Categoria = "Informática",
                        Preco = 4519m,
                        Fabricante = "Dell",
                        Imagens =  new List<Imagem>{
                            new Imagem {
                                Link = "https://images-submarino.b2w.io/produtos/01/00/item/131501/6/131501665_1GG.jpg",
                                Nome = "Notebook 1"
                            },
                            new Imagem {
                                Link = "https://images-submarino.b2w.io/produtos/01/00/item/131501/6/131501665_2SZ.jpg",
                                Nome = "Notebook 2"
                            },
                            new Imagem
                            {
                                Link = "https://images-submarino.b2w.io/produtos/01/00/item/131501/6/131501665_3SZ.jpg",
                                Nome = "Notebook 3"
                            }
                        }
                    },
                    new Produto
                    {
                        Nome = "Notebook Gamer Acer VX5-591G-54PG Intel Core i5 8GB",
                        Descricao = "Notebook Gamer Acer VX5-591G-54PG Intel Core i5 8GB (GeForce GTX 1050 com 4GB) 1TB Tela LED 15,6 Windows 10 - Preto",
                        Categoria = "Informática",
                        Preco= 3329.99m,
                        Fabricante = "Acer",
                        Imagens =  new List<Imagem>{
                            new Imagem {
                                Link = "https://images-submarino.b2w.io/produtos/01/00/item/132134/3/132134369SZ.jpg",
                                Nome = "Notebook 1"
                            },
                            new Imagem {
                                Link = "https://images-submarino.b2w.io/produtos/01/00/item/132134/3/132134369_2SZ.jpg",
                                Nome = "Notebook 2"
                            },
                            new Imagem
                            {
                                Link = "https://images-submarino.b2w.io/produtos/01/00/item/132134/3/132134369_3SZ.jpg",
                                Nome = "Notebook 3"
                            },
                            new Imagem
                            {
                                Link = "https://images-submarino.b2w.io/produtos/01/00/item/132134/3/132134369_4SZ.jpg",
                                Nome = "Notebook 4"
                            }
                        }
                    },
                    new Produto
                    {
                        Nome = "Notebook Acer A515-51-56K6",
                        Descricao = "Notebook Acer A515-51-56K6 Intel Core I5 8GB 1TB Tela LED 15.6 Windows 10 - Preto",
                        Categoria = "Informática",
                        Preco = 2222.21m,
                        Fabricante = "Acer",
                        Imagens =  new List<Imagem>{
                            new Imagem {
                                Link = "https://images-submarino.b2w.io/produtos/01/00/item/132538/3/132538321SZ.jpg",
                                Nome = "Notebook 1"
                            },
                            new Imagem {
                                Link = "https://images-submarino.b2w.io/produtos/01/00/item/132538/3/132538321_2SZ.jpg",
                                Nome = "Notebook 2"
                            },
                            new Imagem
                            {
                                Link = "https://images-submarino.b2w.io/produtos/01/00/item/132538/3/132538321_3SZ.jpg",
                                Nome = "Notebook 3"
                            },
                            new Imagem
                            {
                                Link = "https://images-submarino.b2w.io/produtos/01/00/item/132538/3/132538321_5SZ.jpg",
                                Nome = "Notebook 4"
                            }
                        }
                    },
                    new Produto
                    {
                        Nome = "Thinking Cap",
                        Descricao = "Improve brain efficiency by 75%",
                        Categoria = "Chess",
                        Preco = 16m       
                    },
                    new Produto
                    {
                        Nome = "Unsteady Chair",
                        Descricao = "Secretly give your opponent a disadvantage",
                        Categoria = "Chess",
                        Preco = 29.95m       
                    },
                    new Produto
                    {
                        Nome = "Human Chess Board",
                        Descricao = "A fun game for the family",
                        Categoria = "Chess",
                        Preco = 75m            
                    },
                    new Produto
                    {
                        Nome = "Bling-Bling King",
                        Descricao = "Gold-plated, diamond-studded King",
                        Categoria = "Chess",
                        Preco = 1200m
                    }
                };
            foreach (Produto p in produtos)
            {
                context.Produtos.Add(p);
            }
            context.SaveChanges();
        }
    }
}
