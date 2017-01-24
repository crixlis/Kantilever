using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySQL.Data.EntityFrameworkCore.Extensions;
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
            //.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ArtikelenKantilever;Trusted_Connection=true");
             .UseMySQL(Environment.GetEnvironmentVariable("MYSQL_CONNECTION"));

            return new WebshopContext(builder.Options);
        }
    }
}
