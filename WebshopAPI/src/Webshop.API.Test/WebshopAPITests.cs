using Xunit;
using Webshop.API;
using Webshop.API.Controllers;
using NSubstitute;
using rabbitmq_demo;

namespace Webshop.API.Test
{
    public class WebshopAPITests
    {
        [Fact]
        public void ErKanEenPostMetEenBestellingGedaanWordenWaarnaEenBestellingKeurenCommandWordtOpgegooid()
        {
            //Arrange
            var sender = Substitute.For<ISender>();
            var bestelling = new Bestelling
            {
                Id = 0
            };

            //Act
            var controller = new BestellingenController(sender);
            controller.Post(bestelling);

            //Assert
            sender.Received(1).PublishCommand(Arg.Any<BestellingKeuren>());
        }
    }
}
