using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace FactuurService.Database
{
    public class FactuurServiceContext : DbContext, IFactuurServiceContext
    {
        public FactuurServiceContext(DbContextOptions<FactuurServiceContext> options) : base(options) { }

        public DbSet<Factuur> Facturen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Factuur>()
                .Property(a => a.Id)
                .ValueGeneratedNever();
        }
    }
}
