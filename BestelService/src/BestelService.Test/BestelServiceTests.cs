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
                var bestelling = new BestellingAanmaken { Id = 1 };

                //Act
                service.Execute(bestelling);

                //Assert
                sender.Received(1).PublishEvent(Arg.Any<BestellingAangemaakt>());
            }
        }

        [Fact]
        public void IkWilEenBestellingKeurenEventOpvangenEnInDeDatabaseOpslaan()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<BestelServiceContext>()
               .UseInMemoryDatabase(databaseName: "BestellingKeuren")
               .Options;
            using (var context = new BestelServiceContext(options))
            {
                var sender = Substitute.For<ISender>();
                var service = new BestelService(sender, context);
                var bestelling = new BestellingKeuren { Id = 1 };

                //Act
                service.Execute(bestelling);

                //Assert
                Assert.True(context.Bestelling.Any());
            }
        }

        [Fact]
        public void IkWilEenBestellingGoedgekeurdEventOpvangenEnInDeDatabaseOpslaan()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<BestelServiceContext>()
               .UseInMemoryDatabase(databaseName: "BestellingGoedkeuren")
               .Options;
            using (var context = new BestelServiceContext(options))
            {
                var sender = Substitute.For<ISender>();
                var service = new BestelService(sender, context);
                var bestelling = new BestellingGoedgekeurd { Id = 1 };

                //Act
                service.Execute(bestelling);

                //Assert
                Assert.True(context.Bestelling.Any());
            }
        }
    }
}
