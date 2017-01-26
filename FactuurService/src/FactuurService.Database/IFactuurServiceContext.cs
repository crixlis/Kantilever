using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactuurService.Database
{
    public interface IFactuurServiceContext
    {
        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
        DbSet<Factuur> Facturen { get; set; }
        DbSet<Bestelling> Bestellingen { get; set; }
    }
}
