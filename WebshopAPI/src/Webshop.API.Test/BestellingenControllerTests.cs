using Xunit;
using Webshop.API;
using Webshop.API.Controllers;
using NSubstitute;
using rabbitmq_demo;
using System;

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
                Id = 0
            };

            //Act
            var controller = new BestellingenController(sender);
            controller.Post(bestelling);

            //Assert
            sender.Received(1).PublishCommand(Arg.Any<BestellingKeuren>());
        }

        [Fact]
        public void ErKanEenBestellingOpBasisVanEeenSpecifiekIdOpgevraagdWorden()
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
    }
}
