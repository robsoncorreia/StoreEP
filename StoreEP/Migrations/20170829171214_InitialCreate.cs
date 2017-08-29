using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StoreEP.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    CD_Produto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImagemProduto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NM_Categoria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NM_Produto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PD_Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PD_Fabricante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PD_RelacionadoCD_Produto = table.Column<int>(type: "int", nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.CD_Produto);
                    table.ForeignKey(
                        name: "FK_Produtos_Produtos_PD_RelacionadoCD_Produto",
                        column: x => x.PD_RelacionadoCD_Produto,
                        principalTable: "Produtos",
                        principalColumn: "CD_Produto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_PD_RelacionadoCD_Produto",
                table: "Produtos",
                column: "PD_RelacionadoCD_Produto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
