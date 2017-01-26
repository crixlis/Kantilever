using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System;

namespace BestelService.Database
{
    public class BestelServiceContextFactory : IDbContextFactory<BestelServiceContext>
    {
        public BestelServiceContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<BestelServiceContext>()
             .UseMySQL(@"server=localhost;userid=root;pwd=my-secret-pw;port=9876;database=BestellingenKantilever;sslmode=none;");
             //.UseMySQL(Environment.GetEnvironmentVariable("MYSQL_CONNECTION"));
            return new BestelServiceContext(builder.Options);
        }
    }
}
