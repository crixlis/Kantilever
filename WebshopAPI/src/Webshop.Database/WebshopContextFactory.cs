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
            .UseMySql(@"server=localhost;userid=root;pwd=my-secret-pw;port=3306;database=ArtikelenKantilever;sslmode=none");
            //.UseMySQL(Environment.GetEnvironmentVariable("MYSQL_CONNECTION"));

            return new WebshopContext(builder.Options);
        }
    }
}
