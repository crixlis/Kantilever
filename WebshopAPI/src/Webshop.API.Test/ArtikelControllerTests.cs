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

            //Arrange
            var options = new DbContextOptionsBuilder<WebshopContext>()
                .UseInMemoryDatabase(databaseName: "ArtikelOBVId")
                .Options;

            using (var context = new WebshopContext(options))
            {
                context.Artikelen.Add(new Database.Artikel
                {
                    Id = 5,
                    Naam = "Giant XTC",
                    Beschrijving = "Mountainbike",
                    Prijs = 1000.99m,
                    LeverbaarVanaf = new DateTime(2017, 1, 1),
                    LeverbaarTot = new DateTime(2020, 1, 1),
                    Leverancier = "Giant",
                    Categorieen = new List<string> { "Mountainbikes", "Fietsen" },
                    ImagePath = "/img/root"
                });

                context.SaveChanges();

                var sender = Substitute.For<ISender>();
                var controller = new ArtikelController(sender, context);

                //Act
                var result = controller.Get(5);

                //Assert
                Assert.Equal(5 , result.Id);
                Assert.Equal("Giant XTC", result.Naam);
                Assert.Equal(1000.99m, result.Prijs);
                Assert.Equal(new DateTime(2017, 1, 1), result.LeverbaarVanaf);
                Assert.Equal(new DateTime(2020, 1, 1), result.LeverbaarTot);
                Assert.Equal("Giant", result.Leverancier);
                Assert.Equal("Mountainbikes", result.Categorieen[0]);
                Assert.Equal("/img/root", result.ImagePath);
            }
        }

        [Fact]
        public void AlleArtikelenKunnenInEenKeerOpgevraagdWorden()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<WebshopContext>()
                .UseInMemoryDatabase(databaseName: "ZelfArtikelAanCatalogusToevoegen")
                .Options;

            using (var context = new WebshopContext(options))
            {
                context.Artikelen.Add(new Database.Artikel
                {
                    Id = 889,
                    Naam = "testArtikel",
                    Beschrijving = "testBeschrijving",
                    Categorieen = new List<string> {"testcategorie" },
                    ImagePath = "/img/root",
                    Leverancier = "testLeverancier",
                    LeverancierCode =  "5XC",
                    LeverbaarTot = new DateTime(2018, 2, 28),
                    LeverbaarVanaf = new DateTime(2017, 2, 28),
                    Prijs = 5m,
                    Voorraad = 5
                });

                context.SaveChanges();

                var sender = Substitute.For<ISender>();
                var controller = new ArtikelController(sender, context);

                //Act
                var result = controller.Get();

                //Assert
                Assert.True(result.Count == 1);
                Assert.Equal(889, result[0].Id);
            }
        }
    }
}
