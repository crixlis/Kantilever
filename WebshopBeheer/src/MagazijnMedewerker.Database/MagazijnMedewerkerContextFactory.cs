using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MagazijnMedewerker.Database
{
    public class MagazijnMedewerkerContextFactory : IDbContextFactory<MagazijnMedewerkerContext>
    {
        public MagazijnMedewerkerContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<MagazijnMedewerkerContext>()
            //.UseMySQL(@"server=lmf-webbeheer.database;userid=root;pwd=my-secret-pw;port=3306;database=ArtikelenKantilever;sslmode=none;");
            .UseSqlServer(@"Server=.\SQLEXPRESS;Database=WebShopBeheerMagazijnMedewerker;Trusted_Connection=True");

            return new MagazijnMedewerkerContext(builder.Options);
        }
    }
}
