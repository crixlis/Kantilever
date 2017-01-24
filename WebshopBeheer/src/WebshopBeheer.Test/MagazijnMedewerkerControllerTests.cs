using System.Net;
using Xunit;
using WebshopBeheer.Controllers;
using WebshopBeheer.Database;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace WebshopBeheer.Test
{
    public class MagazijnMedewerkerControllerTests
    {

        [Fact]
        public async void TestOfPaginaBereikbaarIsNaHetStarten()
        {
            using (var server = CustomTestServer.Start())
            using (var client = server.CreateClient())
            {
                var controllerName = nameof(MagazijnMedewerkerController).Replace("Controller", "");
                var status = await client.GetAsync(controllerName + "\\" + "Index");
                Assert.Equal(HttpStatusCode.OK, status.StatusCode);
            }

        }

        [Fact]
        public void WebshopBeheercontextKanEenBestellingOpslaan()
        {
            //Arrange
            var bestelling1 = new Bestelling()
            {
                Id = 1,
                Klant = new Klant() { Id = 10, Achternaam = "Slager" },
                Artikelen = new List<Artikel>()
                {
                    new Artikel() { Id = 5, Beschrijving= "Fietsband", Voorraad = 6 },
                    new Artikel() { Id = 7, Beschrijving= "Fietscomputer", Voorraad = 2 }
                }
            };

            var options = new DbContextOptionsBuilder<WebshopBeheerContext>()
               .UseInMemoryDatabase(databaseName: "MagazijnMedewerkerTest2")
               .Options;

            using (var context = new WebshopBeheerContext(options))
            {
                //Act
                context.Bestellingen.Add(bestelling1);
                context.SaveChanges();

                //Assert
                Assert.True(context.Bestellingen.Contains(bestelling1));
            }
        }


        [Fact]
        public void MagazijnMedewerkerPaginaControllerKanBestellingenLatenZien()
        {
            //Arrange
            var bestelling1 = new Bestelling()
            {
                Id = 1,
                Klant = new Klant() { Id = 10, Achternaam = "Slager" },
                Artikelen = new List<Artikel>()
                {
                    new Artikel() { Id = 5, Beschrijving= "Fietsband", Voorraad = 6 },
                    new Artikel() { Id = 7, Beschrijving= "Fietscomputer", Voorraad = 2 }
                }
            };

            var options = new DbContextOptionsBuilder<WebshopBeheerContext>()
               .UseInMemoryDatabase(databaseName: "MagazijnMedewerkerTest3")
               .Options;

            using (var context = new WebshopBeheerContext(options))
            {
                

                context.Bestellingen.Add(bestelling1);
                context.SaveChanges();

                var controller = new MagazijnMedewerkerController(context);

                //Act
                var view = controller.Index();

                //Act
                var result = Assert.IsType<ViewResult>(view);
                var model = Assert.IsAssignableFrom<IEnumerable<Bestelling>>(result.Model);

                Assert.True(model.Any());
            }

            
        }

        [Fact]
        void MagazijnMedewerkerControllerLaatAlleenBestellingenZienDieNogNietGekeurdZijn()
        {
            //Arrange

            var nognietgekeurdebestelling = new Bestelling()
            {
                Id = 1,
                Status = 0,
                Klant = new Klant() { Id = 10, Achternaam = "Slager" },
                Artikelen = new List<Artikel>()
                {
                    new Artikel() { Id = 5, Beschrijving= "Fietsband", Voorraad = 6 },
                    new Artikel() { Id = 7, Beschrijving= "Fietscomputer", Voorraad = 2 }
                }
            };

            var nognietgekeurdebestelling2 = new Bestelling()
            {
                Id = 4,
                Status = 0,
                Klant = new Klant() { Id = 10, Achternaam = "Slager" },
                Artikelen = new List<Artikel>()
                {
                    new Artikel() { Id = 5, Beschrijving= "Stuur", Voorraad = 6 },
                    new Artikel() { Id = 7, Beschrijving= "Fietscomputer", Voorraad = 2 }
                }
            };

            var gekeurdebestelling = new Bestelling()
            {
                Id = 2,
                Status = 1,
                Klant = new Klant() { Id = 10, Achternaam = "Slager" },
                Artikelen = new List<Artikel>()
                {
                    new Artikel() { Id = 8, Beschrijving= "Zadel", Voorraad = 6 },
                    new Artikel() { Id = 9, Beschrijving= "Ballenknijper met zeemleer", Voorraad = 2 }
                }
            };
            var afgekeurdebestelling = new Bestelling()
            {
                Id = 3,
                Status = 2,
                Klant = new Klant() { Id = 10, Achternaam = "Slager" },
                Artikelen = new List<Artikel>()
                {
                    new Artikel() { Id = 10, Beschrijving= "Fietsbel", Voorraad = 6 },
                    new Artikel() { Id = 11, Beschrijving= "Stuur", Voorraad = 2 }
                }
            };

            var options = new DbContextOptionsBuilder<WebshopBeheerContext>()
               .UseInMemoryDatabase(databaseName: "MagazijnMedewerkerTest5")
               .Options;

            using (var context = new WebshopBeheerContext(options))
            {

                context.Bestellingen.Add(gekeurdebestelling);
                context.Bestellingen.Add(nognietgekeurdebestelling);
                context.Bestellingen.Add(afgekeurdebestelling);
                context.SaveChanges();

                var controller = new MagazijnMedewerkerController(context);

                //Act
                var view = controller.Index();

                //Act
                var result = Assert.IsType<ViewResult>(view);
                var model = Assert.IsAssignableFrom<IEnumerable<Bestelling>>(result.Model);

                Assert.True(model.Count() == 1);
                Assert.True(model.First() == nognietgekeurdebestelling);
            }
        }

        [Fact]
        public void ddd()
        {

        }
    }
}
