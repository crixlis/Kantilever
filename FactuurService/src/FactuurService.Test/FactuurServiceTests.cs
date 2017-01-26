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
        //[Fact]
        //public void DeFactuurServiceKanEenFactuurAanmakenCommandOntvangenEnVerstuurtEenFactuurAangemaaktEvent()
        //{
        //    //Arrange
        //    var options = new DbContextOptionsBuilder<FactuurServiceContext>()
        //        .UseInMemoryDatabase(databaseName: "FacturenKantilever")
        //        .Options;

        //    using (var context = new FactuurServiceContext(options))
        //    {
        //        //context.Database.EnsureCreated();

        //        var sender = Substitute.For<ISender>();
        //        var service = new FactuurService(sender, context);

        //        var FactuurAanmakenCommand = new FactuurAanmaken
        //        {
        //            Id = 56,
        //            Artikelen = new List<Artikel>
        //            {
        //                new Artikel { Id =9, Prijs = 10.95m },
        //                new Artikel { Id =7, Prijs = 12.65m },
        //                new Artikel { Id =11, Prijs = 15.78m }
        //            }
        //        };

        //        //Act
        //        service.Execute(FactuurAanmakenCommand);

        //        //Assert
        //        sender.Received(1).PublishEvent(Arg.Any<Factuur>());
        //    }           
        //}

        //[Fact]
        //public void IkWilFactuurAanmakenEventOpvangenEnInDeDatabaseOpslaan()
        //{
        //    //Arrange
        //    var options = new DbContextOptionsBuilder<FactuurServiceContext>()
        //       .UseInMemoryDatabase(databaseName: "FactuurAanmaken")
        //       .Options;

        //    using (var context = new FactuurServiceContext(options))
        //    {
        //        var sender = Substitute.For<ISender>();
        //        var service = new FactuurService(sender, context);
        //        var factuur = new FactuurAanmaken
        //        {
        //            Id = 1,
        //            Artikelen = new List<Artikel> { new Artikel { Id = 1} }
        //        };

        //        //Act
        //        service.Execute(factuur);

        //        //Assert
        //        Assert.True(context.Facturen.Any());
        //    }
        //}

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
    }
}
