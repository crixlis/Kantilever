using Microsoft.EntityFrameworkCore;
using NSubstitute;
using rabbitmq_demo;
using System;
using System.Collections.Generic;
using Webshop.API.Controllers;
using Webshop.Database;
using Xunit;

namespace Webshop.API.Test
{
    public class ArtikelControllerTests
    {
        [Fact]
        public void ErKanEenArtikelOpBasisVanEeenSpecifiekIdOpgevraagdWorden()
        {
            /* Let op, deze test moest herschreven worden zodra de database actief is. 
            Voor nu wordt het artikel statisch vanuit de controller meegegeven */

            //Arrange

            var options = new DbContextOptionsBuilder<WebshopContext>()
                .UseInMemoryDatabase(databaseName: "ArtikelOBVId")
                .Options;

            using (var context = new WebshopContext(options))
            {

                var sender = Substitute.For<ISender>();
                var controller = new ArtikelController(sender, context);
                var artikelExpected = new Artikel
                {
                    Id = 0,
                    Naam = "Giant XTC",
                    Beschrijving = "Mountainbike",
                    Prijs = 1000.99m,
                    LeverbaarVanaf = new DateTime(2017, 1, 1),
                    LeverbaarTot = new DateTime(2020, 1, 1),
                    Leverancier = "Giant",
                    Categorieen = new List<string> { "Mountainbikes", "Fietsen" },
                    ImagePath = "/img/root"
                };

                //Act
                var artikelFromAPI = controller.Get(0);

                //Assert all properties
                Assert.Equal(artikelExpected.Id, artikelFromAPI.Id);
                Assert.Equal(artikelExpected.Naam, artikelFromAPI.Naam);
                Assert.Equal(artikelExpected.Beschrijving, artikelFromAPI.Beschrijving);
                Assert.Equal(artikelExpected.Prijs, artikelFromAPI.Prijs);
                Assert.Equal(artikelExpected.LeverbaarVanaf, artikelFromAPI.LeverbaarVanaf);
                Assert.Equal(artikelExpected.LeverbaarTot, artikelFromAPI.LeverbaarTot);
                Assert.Equal(artikelExpected.Leverancier, artikelFromAPI.Leverancier);
                Assert.Equal(artikelExpected.Categorieen, artikelFromAPI.Categorieen);
            }
        }

        [Fact]
        public void AlleArtikelenKunnenInEenKeerOpgevraagdWorden()
        {
            /* Let op, deze test moest herschreven worden zodra de database actief is. 
            Voor nu wordt het artikel statisch vanuit de controller meegegeven */

            var options = new DbContextOptionsBuilder<WebshopContext>()
                .UseInMemoryDatabase(databaseName: "ZelfArtikelAanCatalogusToevoegen")
                .Options;

            using (var context = new WebshopContext(options))
            {
                context.Artikelen.Add(new Database.Artikel
                {
                    Id = 889,
                    Naam = "testArtikel"
                });

                context.SaveChanges();
                //Arrange
                var sender = Substitute.For<ISender>();
                var controller = new ArtikelController(sender, context);

                //Act
                var artikelenFromAPI = controller.Get();

                //Assert
                Assert.True(artikelenFromAPI.Count == 1);
                Assert.Equal(889, artikelenFromAPI[0].Id);
                //Assert.Equal(1, artikelenFromAPI[1].Id);

            }
        }
    }
}
