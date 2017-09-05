using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreEP.Models.ViewModels;

namespace StoreEP.Models
{
    public class StoreEPContext : DbContext
    {
        public StoreEPContext (DbContextOptions<StoreEPContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produto { get; set; }
    }
}