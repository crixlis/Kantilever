using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace FactuurService.Database
{
    public class FactuurServiceContextFacory : IDbContextFactory<FactuurServiceContext>
    {
        public FactuurServiceContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<FactuurServiceContext>()
             .UseMySQL(Environment.GetEnvironmentVariable("MYSQL_CONNECTION"));
             //.UseMySQL(@"server=localhost;userid=root;pwd=my-secret-pw;port=567;database=FacturenKantilever;sslmode=none");

            return new FactuurServiceContext(builder.Options);
        }
    }
}
