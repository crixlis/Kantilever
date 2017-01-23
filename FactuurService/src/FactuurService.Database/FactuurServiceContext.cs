using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FactuurService.Database
{
    public class FactuurServiceContext
    {
        public FactuurServiceContext(DbContextOptions<FactuurServiceContext> options) : base(options) { }

        public DbSet<Artikel> Artikelen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Artikel>()
                .Property(a => a.Id)
                .ValueGeneratedNever();
        }
    }
}
