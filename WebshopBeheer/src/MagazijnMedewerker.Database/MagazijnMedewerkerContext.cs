using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazijnMedewerker.Database
{
    public class MagazijnMedewerkerContext : DbContext
    {
        public MagazijnMedewerkerContext(DbContextOptions<MagazijnMedewerkerContext> options) : base(options) { }

        public DbSet<BestelArtikel> BestelArtikelSet { get; set; }
        public DbSet<Bestelling> Bestellingen { get; set; }
        public DbSet<Artikel> Artikelen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Bestelling>()
                .Property(a => a.Id)
                .ValueGeneratedNever();

            modelBuilder
                .Entity<Artikel>()
                .Property(a => a.Id)
                .ValueGeneratedNever();
        }
    }
}
