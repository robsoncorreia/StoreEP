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
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().ToTable("Produto");
            modelBuilder.Entity<Pedido>().ToTable("Pedido");
            modelBuilder.Entity<Endereco>().ToTable("Endereco");
            modelBuilder.Entity<Comentario>().ToTable("Comentario");
            modelBuilder.Entity<Pagamento>().ToTable("Pagamento");
        }
    }
}
