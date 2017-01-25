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
        public void PostMethodGeeftEenBadRequest400TerugBijEenNietCompletePost()
        {
            //Arrange
            var sender = Substitute.For<ISender>();
            var bestelling = new Bestelling
            {
            };

            //Act
            var controller = new BestellingenController(sender);
            var response = (BadRequestObjectResult)controller.Post(bestelling);

            //Assert
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("Er is iets fout gegaan met het toevoegen van het product. Controleer of de bestelling geldig is.", response.Value);
        }

        [Fact]
        public void ErKanEenPostMetEenBestellingGedaanWordenWaarnaEenBestellingAanmakenCommandWordtOpgegooid()
        {
            //Arrange
            var sender = Substitute.For<ISender>();
            var bestelling = new Bestelling
            {
                Id = 1,
                Artikelen = new List<Artikel>
                {
                    new Artikel
                    {
                        Id = 1,
                        Aantal = 2
                    }
                },
                Klant = new Klant
                {
                    Voornaam = "Henk",
                    Achternaam = "Jansen",
                    Plaatsnaam = "Utrecht",
                    Postcode = "3512AA",
                    Telefoonnummer = 030464646,
                    Adres = "Bilstraat 123"
                }
            };

            //Act
            var controller = new BestellingenController(sender);
            var response = (CreatedAtRouteResult)controller.Post(bestelling);

            //Assert
            sender.Received(1).PublishCommand(Arg.Any<BestellingAanmaken>());
            Assert.Equal(201, response.StatusCode);
        }



    }
}
