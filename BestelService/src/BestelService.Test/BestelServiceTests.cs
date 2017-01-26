using BestelService.Database;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using rabbitmq_demo;
using System;
using System.Linq;
using Xunit;

namespace BestelService.Test
{
    public class BestelServiceTests
    {
        [Fact]
        public void IkWilEenBestellingAanmakenEventOpvangenEnEenBestellingAangemaaktPublishen()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<BestelServiceContext>()
               .UseInMemoryDatabase(databaseName: "BestellingAanmaken")
               .Options;

            using (var context = new BestelServiceContext(options))
            {
                var sender = Substitute.For<ISender>();
                var service = new BestelService(sender, context);
                var bestelling = new BestellingAanmaken { };

                //Act
                service.Execute(bestelling);

                //Assert
                sender.Received(1).PublishEvent(Arg.Any<BestellingAangemaakt>());
            }
        }

        [Fact]
        public void IkWilEenBestellingAanmakenEventOpvangenEnInDeDatabaseOpslaan()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<BestelServiceContext>()
               .UseInMemoryDatabase(databaseName: "BestellingAanmakenOpslaanInDB")
               .Options;

            using (var context = new BestelServiceContext(options))
            {
                var sender = Substitute.For<ISender>();
                var service = new BestelService(sender, context);
                var bestelling = new BestellingAanmaken { };

                //Act
                service.Execute(bestelling);

                //Assert
                Assert.True(context.Bestelling.Any());
            }
        }

        [Fact]
        public void IkWilEenBestellingKeurenEventOpvangenEnUpdatenInDeDatabase()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<BestelServiceContext>()
               .UseInMemoryDatabase(databaseName: "BestellingKeurenUpdatenInDB")
               .Options;

            using (var context = new BestelServiceContext(options))
            {
                var sender = Substitute.For<ISender>();
                var service = new BestelService(sender, context);
                var id = 124;

                context.Bestelling.Add(new Bestelling { Id = id,  Status = Status.NogTeKeuren });
                context.SaveChanges();

                var bestellingteUpdaten = context.Bestelling.Where(a => a.Id == id).Single();
                bestellingteUpdaten.Status = Status.GoedGekeurd;

                //Act
                service.Execute( new BestellingKeuren { Id = id, Status = bestellingteUpdaten.Status });

                //Assert
                Assert.True(context.Bestelling.Any());
            }
        }

        [Fact]
        public void IkWilEenBestellingKeurenEventOpvangenEnEenBestellingGoedgekeurdPublishen()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<BestelServiceContext>()
               .UseInMemoryDatabase(databaseName: "BestellingKeurenEnGoedgekeurdePublishen")
               .Options;

            using (var context = new BestelServiceContext(options))
            {
                var sender = Substitute.For<ISender>();
                var service = new BestelService(sender, context);

                var id = 20;
                context.Bestelling.Add(new Bestelling { Id = id, Status = Status.GoedGekeurd });
                context.SaveChanges();

                //Act
                service.Execute(new BestellingKeuren { Id = id, Status = Status.GoedGekeurd });

                //Assert
                sender.Received(1).PublishEvent(Arg.Any<BestellingGoedgekeurd>());
            }
        }

        [Fact]
        public void IkWilEenBestellingKeurenEventOpvangenEnEenBestellingAfgekeurdPublishen()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<BestelServiceContext>()
               .UseInMemoryDatabase(databaseName: "BestellingKeurenEnAfgekeurdePublishen")
               .Options;

            using (var context = new BestelServiceContext(options))
            {
                var sender = Substitute.For<ISender>();
                var service = new BestelService(sender, context);

                var id = 22;
                context.Bestelling.Add(new Bestelling { Id = id, Status = Status.Afgekeurd });
                context.SaveChanges();

                //Act
                service.Execute(new BestellingKeuren { Id = id });

                //Assert
                sender.Received(1).PublishEvent(Arg.Any<BestellingAfgekeurd>());
            }
        }
    }
}
