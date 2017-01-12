﻿using NSubstitute;
using rabbitmq_demo;
using Xunit;

namespace BestelService.Test
{
    public class BestelServiceTests
    {
        [Fact]
        public void IkWilEenBestellingAanmakenEventOpvangenEnEenBestellingAangemaaktPublishen()
        {
            //Arrange
            var sender = Substitute.For<ISender>();
            var service = new BestelService(sender);
            var bestelling = new bestellingAanmaken{Id = 1};

            //Act
            service.Execute(bestelling);

            //Assert
            sender.Received(1).PublishEvent(Arg.Any<bestellingAangemaakt>());
        }
    }
}
