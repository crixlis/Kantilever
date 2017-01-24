using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CommercieelManager.Database
{
    public class CommercieelManagerContextFactory : IDbContextFactory<CommercieelManagerContext>
    {
        public CommercieelManagerContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<CommercieelManagerContext>()
            //.UseMySQL(@"server=lmf-webbeheer.database;userid=root;pwd=my-secret-pw;port=3306;database=ArtikelenKantilever;sslmode=none;");
            .UseSqlServer(@"Server=.\SQLEXPRESS;Database=WebShopBeheerCommercieelManager;Trusted_Connection=True");

            return new CommercieelManagerContext(builder.Options);
        }
    }
}
