using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FactuurService;
using RabbitMQ;
using rabbitmq_demo;
using NSubstitute;
using FactuurService.Database;
using Microsoft.EntityFrameworkCore;

namespace FactuurService.Test
{
    public class FactuurServiceTests
    {
        [Fact]
        public void DeFactuurServiceKanEenFactuurAanmakenCommandOntvangenEnVerstuurtEenFactuurAangemaaktEvent()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<FactuurServiceContext>()
                .UseInMemoryDatabase(databaseName: "FacturenKantilever")
                .Options;

            using (var context = new FactuurServiceContext(options))
            {
                var sender = Substitute.For<ISender>();
                var service = new FactuurService(sender, context);
                var id = 55;

                context.Bestellingen.Add(new Bestelling
                {
                    Id = id,
                    Artikelen = new List<Artikel> { new Artikel { Id = 1, Prijs = 5m } },
                    Klant = new Klant { Id = 1 }
                });
                context.SaveChanges();

                var FactuurAanmakenCommand = new FactuurAanmaken
                {
                    Id = 55
                };

                //Act
                service.Execute(FactuurAanmakenCommand);

                //Assert
                sender.Received(1).PublishEvent(Arg.Any<Factuur>());
            }
        }

        [Fact]
        public void IkWilFactuurAanmakenEventOpvangenEnInDeDatabaseOpslaan()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<FactuurServiceContext>()
               .UseInMemoryDatabase(databaseName: "FactuurAanmaken")
               .Options;

            using (var context = new FactuurServiceContext(options))
            {
                var sender = Substitute.For<ISender>();
                var service = new FactuurService(sender, context);
                var id = 55;

                context.Bestellingen.Add(new Bestelling
                {
                    Id = id,
                    Artikelen = new List<Artikel> { new Artikel { Id = 1, Prijs = 5m } },
                    Klant = new Klant { Id = 1 }
                });
                context.SaveChanges();

                var FactuurAanmakenCommand = new FactuurAanmaken
                {
                    Id = 55
                };

                //Act
                service.Execute(FactuurAanmakenCommand);

                //Assert
                Assert.True(context.Facturen.Any());
            }
        }

        [Fact]
        public void DeFactuurServiceKanEenBetaaldeFactuurAfmeldenCommandOntvangenEnEenBetaaldeFactuurAfgemeldEventOpgooien()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<FactuurServiceContext>()
                .UseInMemoryDatabase(databaseName: "FacturenKantilever")
                .Options;

            using (var context = new FactuurServiceContext(options))
            {
                //context.Database.EnsureCreated();

                var sender = Substitute.For<ISender>();
                var service = new FactuurService(sender, context);

                var BetaaldeFactuurAfmeldenCommand = new BetaaldeFactuurAfmelden
                {
                    Id = 80
                };

                //Act
                service.Execute(BetaaldeFactuurAfmeldenCommand);

                //Assert
                sender.Received(1).PublishEvent(Arg.Any<BetaaldeFactuurAfgemeld>());
            }
        }

        [Fact]
        public void IkWilEenBestellingAangemaaktEventOpvangenEnInDeDatabaseOpslaan()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<FactuurServiceContext>()
               .UseInMemoryDatabase(databaseName: "BestellingAangemaaktInDB")
               .Options;

            using (var context = new FactuurServiceContext(options))
            {
                var sender = Substitute.For<ISender>();
                var service = new FactuurService(sender, context);
                var id = 58;
                             
                //Act
                service.Execute(new BestellingAangemaakt
                {
                    Id = id,
                    Artikelen = new List<Artikel> { new Artikel { Id = 1, Prijs = 5m } },
                    Klant = new Klant { Id = 1 }
                });

                //Assert
                Assert.True(context.Bestellingen.Any());
            }
        }
    }
}
