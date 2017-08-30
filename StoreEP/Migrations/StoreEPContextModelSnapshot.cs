using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using StoreEP.Models;

namespace StoreEP.Migrations
{
    [DbContext(typeof(StoreEPContext))]
    partial class StoreEPContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StoreEP.Models.Produto", b =>
                {
                    b.Property<int>("ProdutoID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoriaPD");

                    b.Property<string>("DescricaoPD");

                    b.Property<string>("Fabricante");

                    b.Property<string>("LinkImagemPD");

                    b.Property<string>("NomePD");

                    b.Property<decimal>("PrecoPD");

                    b.Property<int?>("RelacionadoPDProdutoID");

                    b.HasKey("ProdutoID");

                    b.HasIndex("RelacionadoPDProdutoID");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("StoreEP.Models.Produto", b =>
                {
                    b.HasOne("StoreEP.Models.Produto", "RelacionadoPD")
                        .WithMany()
                        .HasForeignKey("RelacionadoPDProdutoID");
                });
        }
    }
}
