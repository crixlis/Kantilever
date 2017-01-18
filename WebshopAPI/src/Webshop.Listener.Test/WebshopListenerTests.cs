using NSubstitute;
using rabbitmq_demo;
using System;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace Webshop.Listener.Test
{
    public class WebshopListenerTests
    {
        [Fact]
        public void DeWebshopListenerKanEenBetaaldeFactuurAfgemeldEventOntvangen()
        {
            //Arrange
            var sender = Substitute.For<ISender>();
            var service = new WebshopListenerService(sender);

            var factuurAfgemeld = new BetaaldeFactuurAfgemeld
            {
                ID = 0
            };

            //Act + Asser ... er wordt tot nu toe alleen gecontrolleerd of een BetaaldeFactuurAfgemeld event ontvangen kan worden
            Assert.Throws(typeof(NotImplementedException), () => service.Execute(factuurAfgemeld));
        }

        [Fact]

        public void IkWilEenArtikelAanCatalogusToegevoegdEventOpvangenEnOpslaanInDeDatabase()
        {
            var options = new DbContextOptionsBuilder<WebshopContext>()
                .UseInMemoryDatabase(databaseName: "ArtikelAanCatalogusToevoegen")
                .Options;

            using (var context = new WebshopContext(options))
            {

                var sender = Substitute.For<ISender>();
                var service = new WebshopListenerService(sender);

                var artikelToegevoegd = new ArtikelAanCatalogusToegevoegd();
            }
        }
    }
}
