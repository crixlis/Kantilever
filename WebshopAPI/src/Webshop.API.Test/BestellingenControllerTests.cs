using Xunit;
using Webshop.API.Controllers;
using NSubstitute;
using rabbitmq_demo;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Webshop.API.Test
{
    public class BestellingenControllerTests
    {
        [Fact]
        public void ErKanEenPostMetEenBestellingGedaanWordenWaarnaEenBestellingKeurenCommandWordtOpgegooid()
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
                    Acternaam = "Berghuis",
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
                        Categorieen = new string[] { "Mountainbikes", "Fietsen" }
                    }
                }
            };

            //Act
            var controller = new BestellingenController(sender);
            var response = (CreatedAtRouteResult)controller.Post(bestelling);

            //Assert
            sender.Received(1).PublishCommand(Arg.Any<BestellingKeuren>());
            Assert.Equal(201, response.StatusCode);
        }

        [Fact]
        public void PostMethodGeeftEenBadRequest400TerugBijEenNietCompletePost()
        {
            //Arrange
            var sender = Substitute.For<ISender>();
            var bestelling = new Bestelling
            {
                Id = 0
            };

            //Act
            var controller = new BestellingenController(sender);
            var response = (BadRequestObjectResult)controller.Post(bestelling);

            //Assert
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("Er is iets fout gegaan met het toevoegen van het product. Controller of de bestelling compleet is.", response.Value);
        }

        [Fact]
        public void ErKanEenArtikelOpBasisVanEeenSpecifiekIdOpgevraagdWorden()
        {
            /* Let op, deze test moest herschreven worden zodra de database actief is. 
            Voor nu wordt het artikel statisch vanuit de controller meegegeven */

            //Arrange
            var sender = Substitute.For<ISender>();
            var controller = new BestellingenController(sender);
            var artikelExpected = new Artikel
            {
                Id = 0,
                Naam = "Giant XTC",
                Beschrijving = "Mountainbike",
                Prijs = 1000.99m,
                LeverbaarVanaf = new DateTime(2017,1,1),
                LeverbaarTot = new DateTime(2020, 1, 1),
                Leverancier = "Giant",
                Categorieen = new string[] { "Mountainbikes", "Fietsen" }
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

        [Fact]
        public void AlleArtikelenKunnenInEenKeerOpgevraagdWorden()
        {
            /* Let op, deze test moest herschreven worden zodra de database actief is. 
            Voor nu wordt het artikel statisch vanuit de controller meegegeven */

            //Arrange
            var sender = Substitute.For<ISender>();
            var controller = new BestellingenController(sender);

            //Act
            var artikelenFromAPI = controller.Get();

            //Assert
            Assert.True(artikelenFromAPI.Count > 1);
            Assert.Equal(0, artikelenFromAPI[0].Id);
            Assert.Equal(1, artikelenFromAPI[1].Id);
        }
    }
}
