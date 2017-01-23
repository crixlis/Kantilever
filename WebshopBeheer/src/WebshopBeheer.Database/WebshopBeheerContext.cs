using Microsoft.EntityFrameworkCore;

namespace WebshopBeheer.Database
{
    public class WebshopBeheerContext : DbContext
    {
        public WebshopBeheerContext(DbContextOptions<WebshopBeheerContext> options) : base(options) { }

        public DbSet<Bestelling> Bestellingen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Bestelling>()
                .Property(a => a.Id)
                .ValueGeneratedNever();
        }
    }
}