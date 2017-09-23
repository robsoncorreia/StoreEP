using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StoreEP.Models
{
    public class StoreEPContext : DbContext
    {
        public StoreEPContext(DbContextOptions<StoreEPContext> options)
            : base(options)
        {
        }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Pedido> Orders { get; set; }
        public DbSet<Endereco> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().ToTable("Produto");
            modelBuilder.Entity<Pedido>().ToTable("Order");
            modelBuilder.Entity<Endereco>().ToTable("Address");
        }
    }
}
