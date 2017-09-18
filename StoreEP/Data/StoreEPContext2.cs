using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StoreEP.Models
{
    public class StoreEPContext2 : DbContext
    {
        public StoreEPContext2 (DbContextOptions<StoreEPContext2> options)
            : base(options)
        {
        }

        public DbSet<StoreEP.Models.Produto> Produto { get; set; }
    }
}
