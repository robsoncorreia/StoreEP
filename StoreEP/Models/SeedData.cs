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
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            if (!context.Produtos.Any())
            {
                context.Produtos.AddRange(
                    new Produto{
                        NM_Produto = "Kayak",
                        PD_Descricao = "A boat for one person",
                        NM_Categoria = "Watersports", 
                        Preco = 275M
                    },
                    new Produto
                    {
                        NM_Produto = "Lifejacket",
                        PD_Descricao = "Protective and fashionable",
                        NM_Categoria = "Watersports",
                        Preco = 48.95M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
