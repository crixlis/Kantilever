using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public interface IBestelServiceContext
{
    EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
    int SaveChanges();
    DbSet<Bestelling> Bestelling  { get; set; }
}