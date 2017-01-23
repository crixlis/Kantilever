using Xunit;
using NSubstitute;
using Microsoft.EntityFrameworkCore;
using System;
using rabbitmq_demo;
using System.Linq;
using System.Data.SqlClient;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System.IO;

namespace Webshop.Listener.Test
{
    public class WebshopListenerTests
    {
        [Fact]
        public void DeWebshopListenerKanEenBetaaldeFactuurAfgemeldEventOntvangen()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<WebshopContext>()
                .UseInMemoryDatabase(databaseName: "BetaaldeFactuurAfgemeld")
                .Options;

            using (var context = new WebshopContext(options))
            {
                var sender = Substitute.For<ISender>();
                var service = new WebshopListenerService(sender, context, Environment.GetEnvironmentVariable("IMG_ROOT"));

                var factuurAfgemeld = new BetaaldeFactuurAfgemeld
                {
                    ID = 0
                };

                //Act + Assert ... er wordt tot nu toe alleen gecontrolleerd of een BetaaldeFactuurAfgemeld event ontvangen kan worden
                Assert.Throws(typeof(NotImplementedException), () => service.Execute(factuurAfgemeld));
            }
        }

        [Fact]
        public void IkWilEenArtikelAanCatalogusToegevoegdEventOpvangenEnOpslaanInDeDatabase()
        {
            var options = new DbContextOptionsBuilder<WebshopContext>()
                .UseInMemoryDatabase(databaseName: "ArtikelAanCatalogusToevoegen")
                .Options;

            using (var context = new WebshopContext(options))
            {
                //Arrange
                var sender = Substitute.For<ISender>();
                var service = new WebshopListenerService(sender, context, Environment.GetEnvironmentVariable("IMG_ROOT"));
                var artikelToegevoegd = new ArtikelAanCatalogusToegevoegd();

                //Act
                service.Execute(artikelToegevoegd);

                //Assert
                Assert.True(context.Artikelen.Any());
            }
        }

        [Fact]
        public void ZelfToevoegenVanIdVanArtikelAanCatalogusToegevoegdAanDB()
        {
            var options = new DbContextOptionsBuilder<WebshopContext>()
                .UseInMemoryDatabase(databaseName: "ZelfArtikelAanCatalogusToevoegen")
                .Options;

            using (var context = new WebshopContext(options))
            {
                //Arrange
                context.Database.EnsureCreated();

                var id = 34;
                var sender = Substitute.For<ISender>();
                var service = new WebshopListenerService(sender, context, Environment.GetEnvironmentVariable("IMG_ROOT"));

                //Act
                context.Artikelen.Add(new Artikel { Id = id });
                context.SaveChanges();

                //Assert
                Assert.NotEmpty(context.Artikelen.Where(a => a.Id == id));
            }
        }
        
        [Fact]
        public void AlsDePropertyAfbeeldingVanHetInkomendeBerichtLeegIsMoetDeByteArrayNietNaarFileGeschrevenWorden()
        {
            var options = new DbContextOptionsBuilder<WebshopContext>()
                .UseInMemoryDatabase(databaseName: "ZelfArtikelAanCatalogusToevoegen")
                .Options;

            using (var context = new WebshopContext(options))
            {
                var sender = Substitute.For<ISender>();
                var service = new WebshopListenerService(sender, context, Environment.GetEnvironmentVariable("IMG_ROOT"));

                service.Execute(new ArtikelAanCatalogusToegevoegd { Id = 10});
                Environment.SetEnvironmentVariable("IMG_ROOT", "C:\\");
                Assert.False(File.Exists(Path.Combine(Environment.GetEnvironmentVariable("IMG_ROOT"), "10.txt")));
            }
        }
    }
}
