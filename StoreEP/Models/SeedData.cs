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
                        Nome = "Kayak",
                        Descricao = "A boat for one person",
                        Categoria = "Watersports",
                        Preco = 275,
                        Imagens =  new List<Imagem>{
                            new Imagem {
                                Link = "https://www.decathlon.pt/media/838/8387868/big_8573f384-9dcd-44ec-8950-d59ae5afaf03.jpg",
                                Nome = "Kayak1"
                            }
                        }
                    },
                    new Produto
                    {
                        Nome = "Lifejacket",
                        Descricao = "Protective and fashionable",
                        Categoria = "Watersports",
                        Preco = 48.95m
                    },
                    new Produto
                    {
                        Nome = "Soccer Ball",
                        Descricao = "FIFA-approved size and weight",
                        Categoria = "Soccer",
                        Preco = 19.50m
                    },
                    new Produto
                    {
                        Nome = "Corner Flags",
                        Descricao = "Give your playing field a professional touch",
                        Categoria = "Soccer",
                        Preco = 34.95m
                    },
                    new Produto
                    {
                        Nome = "Stadium",
                        Descricao = "Flat-packed 35,000-seat stadium",
                        Categoria = "Soccer",
                        Preco = 79500
                    },
                    new Produto
                    {
                        Nome = "Thinking Cap",
                        Descricao = "Improve brain efficiency by 75%",
                        Categoria = "Chess",
                        Preco = 16
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
                        Preco = 75
                    },
                    new Produto
                    {
                        Nome = "Bling-Bling King",
                        Descricao = "Gold-plated, diamond-studded King",
                        Categoria = "Chess",
                        Preco = 1200
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
