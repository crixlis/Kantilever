using Xunit;
using Webshop.API.Controllers;
using NSubstitute;
using rabbitmq_demo;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshop.Database;

namespace Webshop.API.Test
{
    public class BestellingenControllerTests
    {
        [Fact]
        public void ErKanEenPostMetEenBestellingGedaanWordenWaarnaEenBestellingKeurenCommandWordtOpgegooid()
        {
            var options = new DbContextOptionsBuilder<WebshopContext>()
            .UseInMemoryDatabase(databaseName: "BestellingenAanCatalogusToevoegen")
            .Options;

            using (var context = new WebshopContext(options))
            {
                //Arrange
                var sender = Substitute.For<ISender>();
                var bestelling = new Bestelling
                {
                    Id = 1,
                    Klant = new Klant
                    {
                        Id = 0,
                        Voornaam = "Herman",
                        Achternaam = "Berghuis",
                        Adres = "Antilheldenstraat 1",
                        Postcode = "1740 DD",
                        Plaatsnaam = "Schagen",
                        Telefoonnummer = 0612345678
                    },
                    Artikelen = new List<Artikel>
                {
                    new Artikel
                    {
                        Id = 0,
                        Naam = "Giant XTC",
                        Beschrijving = "Mountainbike",
                        Prijs = 1000.99m,
                        LeverbaarVanaf = new DateTime(2017,1,1),
                        LeverbaarTot = new DateTime(2020, 1, 1),
                        Leverancier = "Giant",
                        Categorieen = new List<string> { "Mountainbikes", "Fietsen" }
                    }
                }
                };

                //Act
                var controller = new BestellingenController(sender, context);
                var response = (CreatedAtRouteResult)controller.Post(bestelling);

                //Assert
                sender.Received(1).PublishCommand(Arg.Any<BestellingKeuren>());
                Assert.Equal(201, response.StatusCode);
            }          
        }

        [Fact]
        public void PostMethodGeeftEenBadRequest400TerugBijEenNietCompletePost()
        {

            //Arrange
            var options = new DbContextOptionsBuilder<WebshopContext>()
            .UseInMemoryDatabase(databaseName: "BestellingenAanCatalogusToevoegen")
            .Options;

            using (var context = new WebshopContext(options))
            {
                var sender = Substitute.For<ISender>();
                var bestelling = new Bestelling
                {
                    Id = 0
                };

                //Act
                var controller = new BestellingenController(sender, context);
                var response = (BadRequestObjectResult)controller.Post(bestelling);

                //Assert
                Assert.Equal(400, response.StatusCode);
                Assert.Equal("Er is iets fout gegaan met het toevoegen van het product. Controleer of de bestelling compleet is.", response.Value);
            }
        }

        [Fact]
        public void EenBestellingAanDeDatabaseToevoegen()
        {
            var options = new DbContextOptionsBuilder<WebshopContext>()
            .UseInMemoryDatabase(databaseName: "BestellingenAanCatalogusToevoegen")
            .Options;

            using (var context = new WebshopContext(options))
            {
                var sender = Substitute.For<ISender>();
                var bestelling = new Bestelling
                {
                    Klant = new Klant
                    {
                        Voornaam = "Piet",
                        Achternaam = "Van der Petterflet",
                        Adres = "Petterflet",
                        Id = 865,
                    },
                    Artikelen = new List<Artikel>
                    {
                        new Artikel
                        {
                            Id = 234
                        }
                    }
                };

                //Act
                var controller = new BestellingenController(sender, context);

                controller.Post(bestelling);


            }
        }    
    }
}
