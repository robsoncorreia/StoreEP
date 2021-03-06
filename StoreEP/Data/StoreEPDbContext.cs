using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StoreEP.Models
{
    public class StoreEPDbContext : DbContext
    {
        public StoreEPDbContext(DbContextOptions<StoreEPDbContext> options)
            : base(options)
        {
        }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Imagem> Imagens { get; set; }
        public DbSet<HistoricoPreco> HistoricoPrecos { get; set; }
        public DbSet<ProdutoVisitado> ProdutosVisitados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().ToTable("Produto");
            modelBuilder.Entity<Pedido>().ToTable("Pedido");
            modelBuilder.Entity<Endereco>().ToTable("Endereco");
            modelBuilder.Entity<Avaliacao>().ToTable("Avaliacao");
            modelBuilder.Entity<Pagamento>().ToTable("Pagamento");
            modelBuilder.Entity<Imagem>().ToTable("Imagem");
            modelBuilder.Entity<HistoricoPreco>().ToTable("HistoricoPreco");
            modelBuilder.Entity<ProdutoVisitado>().ToTable("ProdutosVisitados");
        }
    }
}
