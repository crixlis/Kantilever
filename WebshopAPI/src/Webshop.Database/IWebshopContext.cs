using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public interface IWebshopContext
{
    EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
    int SaveChanges();
    DbSet<Artikel> Artikelen { get; set; }
}