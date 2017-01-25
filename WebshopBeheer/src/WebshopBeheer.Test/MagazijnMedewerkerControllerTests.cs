using System.Net;
using Xunit;
using WebshopBeheer.Controllers;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System;
using MagazijnMedewerker.Database;

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
        public void WebshopBeheercontextKanEenBestellingOpslaanVoorMagazijnMedewerker()
        {
            var now = DateTime.Now;

            //Arrange
            var bestelling1 = new MagazijnMedewerker.Database.Bestelling
            {
                Id = 1,
                Status = MagazijnMedewerker.Database.Status.NogTeKeuren,
                Klant = new Klant() { Id = 10, Achternaam = "Slager" },
                BestelDatum = now
            };

            var options = new DbContextOptionsBuilder<MagazijnMedewerkerContext>()
               .UseInMemoryDatabase(databaseName: "MagazijnMedewerkerTest2")
               .Options;

            using (var context = new MagazijnMedewerkerContext(options))
            {
                //Arrange



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
            var now = DateTime.Now;

            //Arrange
            var bestelling1 = new Bestelling()
            {
                Id = 1,
                Status = Status.GoedGekeurd,
                Klant = new Klant() { Id = 10, Achternaam = "Slager" },
                BestelDatum = DateTime.Now
            };

            var options = new DbContextOptionsBuilder<MagazijnMedewerkerContext>()
               .UseInMemoryDatabase(databaseName: "MagazijnMedewerkerTest3")
               .Options;

            using (var context = new MagazijnMedewerkerContext(options))
            {

                context.Bestellingen.Add(bestelling1);
                context.SaveChanges();

                var controller = new MagazijnMedewerkerController(context);

                //Act
                var view = controller.Index();

                //Act
                var result = Assert.IsType<ViewResult>(view);
                var model = Assert.IsType<Models.MagazijnMedewerkerModels.BestellingViewModel>(result.Model);

                Assert.True(model != null);
            }
        }

        [Fact]
        void MagazijnMedewerkerControllerLaatAlleenBestellingenZienDieNogGoedgekeurdZijn()
        {
            //Arrange
            var now = DateTime.Now;

            var nognietgekeurdebestelling = new Bestelling()
            {
                Id = 1,
                Status = Status.NogTeKeuren,
                Klant = new Klant() { Id = 10, Achternaam = "Slager" },
                BestelDatum = now
            };

            var gekeurdebestelling = new Bestelling()
            {
                Id = 2,
                Status = Status.GoedGekeurd,
                Klant = new Klant() { Id = 10, Achternaam = "Slager" },
                BestelDatum = now
            };
            var afgekeurdebestelling = new Bestelling()
            {
                Id = 3,
                Status = Status.Afgekeurd,
                Klant = new Klant() { Id = 10, Achternaam = "Slager" },
                BestelDatum = now
            };

            var options = new DbContextOptionsBuilder<MagazijnMedewerkerContext>()
               .UseInMemoryDatabase(databaseName: "MagazijnMedewerkerTest5")
               .Options;

            using (var context = new MagazijnMedewerkerContext(options))
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
                var model = Assert.IsType<Models.MagazijnMedewerkerModels.BestellingViewModel>(result.Model);

                Assert.True(model.Id == gekeurdebestelling.Id);
            }
        }

        [Fact]
        public void MagazijnMedewerkerControllerLaatMaar1GoedGekeurdeBestellingZien()
        {

            //Arrange
            var goedGekeurdebestelling = new Bestelling()
            {
                Id = 1,
                Status = Status.GoedGekeurd,
                Klant = new Klant() { Id = 10, Achternaam = "Slager" }, 
            };
           var  goedGekeurdebestelling2 = new Bestelling()
            {
                Id = 4,
                Status = Status.GoedGekeurd,
                Klant = new Klant() { Id = 10, Achternaam = "Slager" },
            };

            
            var options = new DbContextOptionsBuilder<MagazijnMedewerkerContext>()
               .UseInMemoryDatabase(databaseName: "MagazijnMedewerkerTest8")
               .Options;

            using (var context = new MagazijnMedewerkerContext(options))
            {

                context.Bestellingen.Add(goedGekeurdebestelling);
                context.Bestellingen.Add(goedGekeurdebestelling2);
                context.SaveChanges();

                var controller = new MagazijnMedewerkerController(context);

                //Act
                var view = controller.Index();

                //Act
                var result = Assert.IsType<ViewResult>(view);
                var model = Assert.IsType<Models.MagazijnMedewerkerModels.BestellingViewModel>(result.Model); //And not IEnumerable

            }

        }
    }
}
