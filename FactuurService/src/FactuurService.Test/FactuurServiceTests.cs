using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FactuurService;
using RabbitMQ;
using rabbitmq_demo;
using NSubstitute;

namespace FactuurService.Test
{
    public class FactuurServiceTests
    {
        [Fact]
        public void DeFactuurServiceKanEenFactuurAanmakenCommandOntvangenEnVerstuurtEenFactuurAangemaaktEvent()
        {
            //Arrange
            var sender = Substitute.For<ISender>();
            var service = new FactuurService(sender);

            var FactuurAanmakenCommand = new BetaaldeFactuurAfmelden
            {
                Id = 0
            };

            //Act
            service.Execute(FactuurAanmakenCommand);

            //Assert
            sender.Received(1).PublishEvent(Arg.Any<FactuurAangemaakt>());
        }

        [Fact]
        public void DeFactuurServiceKanEenBetaaldeFactuurAfmeldenCommandOntvangenEnEenBetaaldeFactuurAfgemeldEventOpgooien()
        {
            //Arrange
            var sender = Substitute.For<ISender>();
            var service = new FactuurService(sender);

            var BetaaldeFactuurAfmeldenCommand = new BetaaldeFactuurAfmelden
            {
                Id = 0
            };

            //Act
            service.Execute(BetaaldeFactuurAfmeldenCommand);

            //Assert
            sender.Received(1).PublishEvent(Arg.Any<BetaaldeFactuurAfgemeld>());
        }
    }
}
