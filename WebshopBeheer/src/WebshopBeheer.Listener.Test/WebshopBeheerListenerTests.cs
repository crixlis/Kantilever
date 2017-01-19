using Microsoft.EntityFrameworkCore;
using NSubstitute;
using rabbitmq_demo;
using System;
using System.Linq;
using Xunit;
using WebshopBeheer.Database;

namespace WebshopBeheer.Listener.Test
{
    public class WebshopBeheerListenerTests
    {

        [Fact]
        public void IkWilEenBestellingKeurenCommandOpvangenEnDeBestellingOpslaanInDeDatabase()
        {
            var options = new DbContextOptionsBuilder<WebshopBeheerContext>()
                .UseInMemoryDatabase(databaseName: "ArtikelAanCatalogusToevoegen")
                .Options;

            using (var context = new WebshopBeheerContext(options))
            {
                //Arrange
                var sender = Substitute.For<ISender>();
                var service = new WebshopBeheerService(sender, context);
                var bestellingKeuren = new BestellingKeuren { Id = 1 };

                //Act
                service.Execute(bestellingKeuren);

                //Assert
                Assert.True(context.Bestellingen.Any());
            }
        }

        [Fact]
        public void IkWilEenBestellingGoedgekeurdEventOpvangenEnEenfactuurAanmakenPublishen()
        {
            //Arrange
            var sender = Substitute.For<ISender>();
            var service = new WebshopBeheerService(sender);
            var bestelling = new BestellingGoedgekeurd{ Id = 1 };

            //Act
            service.Execute(bestelling);

            //Assert
            sender.Received(1).PublishCommand(Arg.Any<FactuurAanmaken>());
        }

        [Fact]
        public void IkWilfactuurAangemaaktEventOpvangenEnEenbetaaldeFactuurAfmeldenPublishen()
        {
            //Arrange
            var sender = Substitute.For<ISender>();
            var service = new WebshopBeheerService(sender);
            var factuur = new FactuurAangemaakt { Id = 1 };

            //Act
            service.Execute(factuur);

            //Assert
            sender.Received(1).PublishCommand(Arg.Any<BetaaldeFactuurAfmelden>());
        }

        [Fact]
        public void IkWilEenbetaaldeFactuurAfgemeldEventOpvangen()
        {
            //Arrange
            var sender = Substitute.For<ISender>();
            var service = new WebshopBeheerService(sender);
            var factuur = new BetaaldeFactuurAfgemeld { Id = 1  };

            //Act and Assert
            Assert.Throws(typeof(NotImplementedException), () => service.Execute(factuur));
        }
    }
}
