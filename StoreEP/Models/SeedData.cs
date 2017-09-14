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

        public static void EnsurePopulated(StoreEPContext context)
        {
            context.Database.EnsureCreated();

            if (context.Produto.Any())
            {
                return;   // DB has been seeded
            }
            var produtos = new Produto[] {
                    new Produto
                    {
                        NomePD = "Kayak",
                        DescricaoPD = "A boat for one person",
                        CategoriaPD = "Watersports",
                        PrecoPD = 275
                    },
                    new Produto
                    {
                        NomePD = "Lifejacket",
                        DescricaoPD = "Protective and fashionable",
                        CategoriaPD = "Watersports",
                        PrecoPD = 48.95m
                    },
                    new Produto
                    {
                        NomePD = "Soccer Ball",
                        DescricaoPD = "FIFA-approved size and weight",
                        CategoriaPD = "Soccer",
                        PrecoPD = 19.50m
                    },
                    new Produto
                    {
                        NomePD = "Corner Flags",
                        DescricaoPD = "Give your playing field a professional touch",
                        CategoriaPD = "Soccer",
                        PrecoPD = 34.95m
                    },
                    new Produto
                    {
                        NomePD = "Stadium",
                        DescricaoPD = "Flat-packed 35,000-seat stadium",
                        CategoriaPD = "Soccer",
                        PrecoPD = 79500
                    },
                    new Produto
                    {
                        NomePD = "Thinking Cap",
                        DescricaoPD = "Improve brain efficiency by 75%",
                        CategoriaPD = "Chess",
                        PrecoPD = 16
                    },
                    new Produto
                    {
                        NomePD = "Unsteady Chair",
                        DescricaoPD = "Secretly give your opponent a disadvantage",
                        CategoriaPD = "Chess",
                        PrecoPD = 29.95m
                    },
                    new Produto
                    {
                        NomePD = "Human Chess Board",
                        DescricaoPD = "A fun game for the family",
                        CategoriaPD = "Chess",
                        PrecoPD = 75
                    },
                    new Produto
                    {
                        NomePD = "Bling-Bling King",
                        DescricaoPD = "Gold-plated, diamond-studded King",
                        CategoriaPD = "Chess",
                        PrecoPD = 1200
                    }
                };
            foreach (Produto p in produtos)
            {
                context.Produto.Add(p);
            }
            context.SaveChanges();

        }
    }
}
