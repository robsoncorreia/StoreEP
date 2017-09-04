﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using StoreEP.Models;
using System;

namespace StoreEP.Migrations
{
    [DbContext(typeof(StoreEPContext))]
    partial class StoreEPContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
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
#pragma warning restore 612, 618
        }
    }
}
