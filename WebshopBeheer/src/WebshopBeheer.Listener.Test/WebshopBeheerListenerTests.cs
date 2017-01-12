using NSubstitute;
using rabbitmq_demo;
using System;
using Xunit;

namespace WebshopBeheer.Listener.Test
{
    public class WebshopBeheerListenerTests
    {
        [Fact]
        public void IkWilEenbestellingGoedgekeurdEventOpvangenEnEenfactuurAanmakenPublishen()
        {
            //Arrange
            var sender = Substitute.For<ISender>();
            var service = new WebshopBeheerService(sender);
            var bestelling = new bestellingGoedgekeurd{ Id = 1 };

            //Act
            service.Execute(bestelling);

            //Assert
            sender.Received(1).PublishEvent(Arg.Any<factuurAanmaken>());
        }

        [Fact]
        public void IkWilfactuurAangemaaktEventOpvangenEnEenbetaaldeFactuurAfmeldenPublishen()
        {
            //Arrange
            var sender = Substitute.For<ISender>();
            var service = new WebshopBeheerService(sender);
            var factuur = new factuurAangemaakt { Id = 1 };

            //Act
            service.Execute(factuur);

            //Assert
            sender.Received(1).PublishEvent(Arg.Any<betaaldeFactuurAfmelden>());
        }

        [Fact]
        public void IkWilEenbetaaldeFactuurAfgemeldEventOpvangen()
        {
            //Arrange
            var sender = Substitute.For<ISender>();
            var service = new WebshopBeheerService(sender);
            var factuur = new betaaldeFactuurAfgemeld { Id = 1  };

            //Act and Assert
            Assert.Throws(typeof(NotImplementedException), () => service.Execute(factuur));
        }
    }
}
