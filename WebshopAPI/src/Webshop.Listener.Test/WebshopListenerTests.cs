using Xunit;
using NSubstitute;
using Microsoft.EntityFrameworkCore;
using System;
using rabbitmq_demo;
using System.Linq;
using System.Data.SqlClient;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace Webshop.Listener.Test
{
    public class WebshopListenerTests
    {
        [Fact]
        public void DeWebshopListenerKanEenBetaaldeFactuurAfgemeldEventOntvangen()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<WebshopContext>()
                .UseInMemoryDatabase(databaseName: "BetaaldeFactuurAfgemeld")
                .Options;

            using (var context = new WebshopContext(options))
            {
                var sender = Substitute.For<ISender>();
                var service = new WebshopListenerService(sender, context);

                var factuurAfgemeld = new BetaaldeFactuurAfgemeld
                {
                    ID = 0
                };

                //Act + Assert ... er wordt tot nu toe alleen gecontrolleerd of een BetaaldeFactuurAfgemeld event ontvangen kan worden
                Assert.Throws(typeof(NotImplementedException), () => service.Execute(factuurAfgemeld));
            }
        }

        [Fact]
        public void IkWilEenArtikelAanCatalogusToegevoegdEventOpvangenEnOpslaanInDeDatabase()
        {
            var options = new DbContextOptionsBuilder<WebshopContext>()
                .UseInMemoryDatabase(databaseName: "ArtikelAanCatalogusToevoegen")
                .Options;

            using (var context = new WebshopContext(options))
            {
                //Arrange
                var sender = Substitute.For<ISender>();
                var service = new WebshopListenerService(sender, context);
                var artikelToegevoegd = new ArtikelAanCatalogusToegevoegd();

                //Act
                service.Execute(artikelToegevoegd);

                //Assert
                Assert.True(context.Artikelen.Any());
            }
        }


        [Fact]
        public void ZelfToevoegenVanIdVanArtikelAanCatalogusToegevoegdAanDB()
        {
            var options = new DbContextOptionsBuilder<WebshopContext>()
                //.UseMySQL(@"server=lmf-webfrontend.api.database;userid=root;pwd=my-secret-pw;port=7568;database=ArtikelenKantilever;sslmode=none;")
                .UseMySQL(@"server=127.0.0.1;userid=root;pwd=my-secret-pw;port=7568;database=ArtikelenKantilever;sslmode=none;")
                .Options;


            using (var context = new WebshopContext(options))
            using (var trans = context.Database.BeginTransaction())
            {
                //Arrange
                context.Database.EnsureCreated();

                var id = 34;
                var sender = Substitute.For<ISender>();
                var service = new WebshopListenerService(sender, context);

                //Act
                context.Artikelen.Add(new Artikel { Id = id });
                context.SaveChanges();

                //Assert
                Assert.NotEmpty(context.Artikelen.Where(a => a.Id == id));
            }
        }
    }
}
