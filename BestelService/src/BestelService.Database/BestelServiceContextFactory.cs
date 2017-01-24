using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestelService.Database
{
    public class BestelServiceContextFactory : IDbContextFactory<BestelServiceContext>
    {
        public BestelServiceContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<BestelServiceContext>()
             .UseMySQL(@"server=127.0.0.1;userid=root;pwd=my-secret-pw;port=3306;database=ArtikelenKantilever;sslmode=none;");
             //.UseMySQL(Environment.GetEnvironmentVariable("MYSQL_CONNECTION"));
            return new BestelServiceContext(builder.Options);
        }
    }
}
