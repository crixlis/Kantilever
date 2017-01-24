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
                context.Database.EnsureCreated();


                var sender = Substitute.For<ISender>();
                var service = new FactuurService(sender, context);

                var FactuurAanmakenCommand = new FactuurAanmaken
                {
                    Id = 0,
                    Artikelen = new List<Artikel>
                    {
                        new Artikel { Id =1, Prijs = 10.95m },
                        new Artikel { Id =2, Prijs = 12.65m },
                        new Artikel { Id =3, Prijs = 15.78m }
                    }
                };

                //Act
                service.Execute(FactuurAanmakenCommand);

                //Assert
                sender.Received(1).PublishEvent(Arg.Any<FactuurAangemaakt>());
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
                context.Database.EnsureCreated();


                var sender = Substitute.For<ISender>();
                var service = new FactuurService(sender, context);

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
}
