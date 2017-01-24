using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BestelService.Database
{
    public class BestelServiceContext : DbContext, IBestelServiceContext
    {
        public BestelServiceContext(DbContextOptions<BestelServiceContext> options) : base(options) { }

        public DbSet<Bestelling> Bestelling { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Bestelling>()
                .Property(a => a.Id)
                .ValueGeneratedNever();
        }
    }
}
