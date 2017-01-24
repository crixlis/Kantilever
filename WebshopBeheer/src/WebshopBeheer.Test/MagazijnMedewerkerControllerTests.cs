using System.Net;
using Xunit;
using WebshopBeheer.Controllers;
using WebshopBeheer.Database;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebshopBeheer.Test
{
    public class MagazijnMedewerkerControllerTests
    {
        private Bestelling bestelling1 = new Bestelling()
        {
            Id = 1,
            Klant = new Klant()
            {
                Id = 10,
                Achternaam = "Slager"
            },
            Artikelen = new List<Artikel>()
                {
                    new Artikel()
                    {
                        Id = 5,
                        Beschrijving= "Fietsband",
                        Voorraad = 6
                    },
                    new Artikel()
                    {
                        Id = 7,
                        Beschrijving= "Fietscomputer",
                        Voorraad = 2
                    }
                }
        };


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
            

            var options = new DbContextOptionsBuilder<WebshopBeheerContext>()
               .UseInMemoryDatabase(databaseName: "MagazijnMedewerkerTest2")
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
    }
}
