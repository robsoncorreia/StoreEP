using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StoreEP.Migrations
{
    public partial class inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    EnderecoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bairro = table.Column<string>(nullable: false),
                    CEP = table.Column<string>(nullable: false),
                    Cidade = table.Column<string>(nullable: false),
                    Complemento = table.Column<string>(nullable: true),
                    DataUtilizacao = table.Column<DateTime>(nullable: false),
                    Estado = table.Column<string>(nullable: false),
                    Numero = table.Column<string>(nullable: false),
                    Pais = table.Column<string>(nullable: false),
                    Rua = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.EnderecoID);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoPreco",
                columns: table => new
                {
                    HistoricoPrecoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataAltarecao = table.Column<DateTime>(nullable: false),
                    PrecoAntigo = table.Column<decimal>(nullable: false),
                    PrecoNovo = table.Column<decimal>(nullable: false),
                    ProdutoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoPreco", x => x.HistoricoPrecoID);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    ProdutoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Categoria = table.Column<string>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    Fabricante = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    Preco = table.Column<decimal>(nullable: false),
                    Publicado = table.Column<bool>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.ProdutoID);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    PedidoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataCompra = table.Column<DateTime>(nullable: false),
                    EnderecoID = table.Column<int>(nullable: true),
                    Enviado = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.PedidoID);
                    table.ForeignKey(
                        name: "FK_Pedido_Endereco_EnderecoID",
                        column: x => x.EnderecoID,
                        principalTable: "Endereco",
                        principalColumn: "EnderecoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacao",
                columns: table => new
                {
                    AvaliacaoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aprovado = table.Column<bool>(nullable: false),
                    AvaliacaoID1 = table.Column<int>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false),
                    Estrela = table.Column<byte>(nullable: false),
                    NomeUsuario = table.Column<string>(nullable: true),
                    ProdutoID = table.Column<int>(nullable: false),
                    Texto = table.Column<string>(nullable: false),
                    Titulo = table.Column<string>(nullable: false),
                    UsuarioID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacao", x => x.AvaliacaoID);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Avaliacao_AvaliacaoID1",
                        column: x => x.AvaliacaoID1,
                        principalTable: "Avaliacao",
                        principalColumn: "AvaliacaoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Produto_ProdutoID",
                        column: x => x.ProdutoID,
                        principalTable: "Produto",
                        principalColumn: "ProdutoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Imagem",
                columns: table => new
                {
                    ImagemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Link = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    ProdutoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagem", x => x.ImagemID);
                    table.ForeignKey(
                        name: "FK_Imagem_Produto_ProdutoID",
                        column: x => x.ProdutoID,
                        principalTable: "Produto",
                        principalColumn: "ProdutoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutosVisitados",
                columns: table => new
                {
                    ProdutoVisitadoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataHoraVisita = table.Column<DateTime>(nullable: false),
                    ProdutoID = table.Column<int>(nullable: true),
                    QuantidadeVisita = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosVisitados", x => x.ProdutoVisitadoID);
                    table.ForeignKey(
                        name: "FK_ProdutosVisitados_Produto_ProdutoID",
                        column: x => x.ProdutoID,
                        principalTable: "Produto",
                        principalColumn: "ProdutoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartLine",
                columns: table => new
                {
                    CartLineID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PedidoID = table.Column<int>(nullable: true),
                    ProdutoID = table.Column<int>(nullable: true),
                    Quantidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartLine", x => x.CartLineID);
                    table.ForeignKey(
                        name: "FK_CartLine_Pedido_PedidoID",
                        column: x => x.PedidoID,
                        principalTable: "Pedido",
                        principalColumn: "PedidoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartLine_Produto_ProdutoID",
                        column: x => x.ProdutoID,
                        principalTable: "Produto",
                        principalColumn: "ProdutoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pagamento",
                columns: table => new
                {
                    PagamentoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataCompra = table.Column<DateTime>(nullable: false),
                    DataPagamento = table.Column<DateTime>(nullable: false),
                    PedidoId = table.Column<int>(nullable: false),
                    TipoPagamento = table.Column<byte>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamento", x => x.PagamentoID);
                    table.ForeignKey(
                        name: "FK_Pagamento_Pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedido",
                        principalColumn: "PedidoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_AvaliacaoID1",
                table: "Avaliacao",
                column: "AvaliacaoID1");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_ProdutoID",
                table: "Avaliacao",
                column: "ProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_PedidoID",
                table: "CartLine",
                column: "PedidoID");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_ProdutoID",
                table: "CartLine",
                column: "ProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_Imagem_ProdutoID",
                table: "Imagem",
                column: "ProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_PedidoId",
                table: "Pagamento",
                column: "PedidoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_EnderecoID",
                table: "Pedido",
                column: "EnderecoID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosVisitados_ProdutoID",
                table: "ProdutosVisitados",
                column: "ProdutoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacao");

            migrationBuilder.DropTable(
                name: "CartLine");

            migrationBuilder.DropTable(
                name: "HistoricoPreco");

            migrationBuilder.DropTable(
                name: "Imagem");

            migrationBuilder.DropTable(
                name: "Pagamento");

            migrationBuilder.DropTable(
                name: "ProdutosVisitados");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Endereco");
        }
    }
}
