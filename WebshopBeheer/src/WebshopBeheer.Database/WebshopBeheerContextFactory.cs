using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySQL.Data.EntityFrameworkCore.Extensions;


namespace WebshopBeheer.Database
{
    public class WebshopBeheerContextFactory : IDbContextFactory<WebshopBeheerContext>
    {
        public WebshopBeheerContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<WebshopBeheerContext>()
             .UseMySQL(@"server=lmf-webbeheer.database;userid=root;pwd=my-secret-pw;port=3306;database=ArtikelenKantilever;sslmode=none;");

            return new WebshopBeheerContext(builder.Options);
        }
    }
}
