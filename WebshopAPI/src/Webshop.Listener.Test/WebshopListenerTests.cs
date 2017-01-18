using Xunit;
using NSubstitute;
using Microsoft.EntityFrameworkCore;
using System;
using rabbitmq_demo;
using System.Linq;

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


    }
}
