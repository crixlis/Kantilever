using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Webshop.Database
{
    public interface IWebshopContext
    {
        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
        DbSet<Artikel> Artikelen { get; set; }
    }
}