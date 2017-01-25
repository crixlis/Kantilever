using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BestelService.Database
{
    public interface IBestelServiceContext
    {
        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
        DbSet<Bestelling> Bestelling { get; set; }
    }
}