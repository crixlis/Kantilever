using Microsoft.EntityFrameworkCore;

namespace Webshop.Database
{
    public class WebshopContext : DbContext, IWebshopContext
    {
        public WebshopContext(DbContextOptions<WebshopContext> options) : base(options) { }

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
