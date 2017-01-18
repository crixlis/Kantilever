using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Database
{
    public class WebshopContextFactory : IDbContextFactory<WebshopContext>
    {
        public WebshopContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<WebshopContext>()
            .UseSqlServer(@"Server=.\SQLEXPRESS;Database=Afspraken;Trusted_Connection=true");

            return new WebshopContext(builder.Options);
        }
    }
}
