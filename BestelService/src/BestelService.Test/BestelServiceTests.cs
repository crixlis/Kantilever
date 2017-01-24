using BestelService.Database;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using rabbitmq_demo;
using System.Linq;
using Xunit;

namespace BestelService.Test
{
    public class BestelServiceTests
    {
        [Fact]
        public void IkWilEenBestellingAanmakenEventOpvangenEnEenBestellingKeurenPublishen()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<BestelServiceContext>()
               .UseInMemoryDatabase(databaseName: "BestellingAanmaken")
               .Options;

            using (var context = new BestelServiceContext(options))
            {
                var sender = Substitute.For<ISender>();
                var service = new BestelService(sender, context);
                var bestelling = new BestellingAanmaken { Id = 1 };

                //Act
                service.Execute(bestelling);

                //Assert
                sender.Received(1).PublishEvent(Arg.Any<BestellingKeuren>());
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
                var bestelling = new BestellingAanmaken { Id = 1 };

                //Act
                service.Execute(bestelling);

                //Assert
                Assert.True(context.Bestelling.Any());
            }
        }

        [Fact]
        public void IkWilEenBestellingGoedgekeurdEventOpvangenEnUpdatenInDeDatabase()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<BestelServiceContext>()
               .UseInMemoryDatabase(databaseName: "BestellingGoedkeurenOpslaanInDB")
               .Options;

            using (var context = new BestelServiceContext(options))
            {
                var sender = Substitute.For<ISender>();
                var service = new BestelService(sender, context);
                var bestelling = new BestellingGoedgekeurd { Id = 1 };

                //Act
                context.Bestelling.Add(new Bestelling { Id = 1});
                context.SaveChanges();

                service.Execute(bestelling);

                //Assert
                Assert.True(context.Bestelling.Any());
            }
        }
    }
}
