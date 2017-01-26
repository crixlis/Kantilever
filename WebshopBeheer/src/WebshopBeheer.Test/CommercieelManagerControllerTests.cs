using CommercieelManager.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using WebshopBeheer.Controllers;
using Xunit;

namespace WebshopBeheer.Test
{
    public class CommercieelManagerControllerTests
    {
        [Fact]
        public async void TestOfPaginaBereikbaarIsNaHetStarten()
        {
            using (var server = CustomTestServer.Start())
            using (var client = server.CreateClient())
            {
                var controllerName = nameof(CommercieelManagerController).Replace("Controller","");
                var status = await client.GetAsync(controllerName + "\\" + "Index");
                Assert.Equal(HttpStatusCode.OK, status.StatusCode);
            }

        }

        [Fact]
        public void CommercieelManagerControllerGeeftVolledigeeDataTerug()
        {
            var options = new DbContextOptionsBuilder<CommercieelManagerContext>()
               .UseInMemoryDatabase(databaseName: "Commercieelmanagertestt3")
               .Options;

            using (var context = new CommercieelManagerContext(options))
            {

                Artikel artikel1 = new Artikel
                {
                    Id = 1,
                    Naam = "Fiets",
                    Prijs = 100
                };
                Artikel artikel2 = new Artikel
                {
                    Id = 2,
                    Naam = "Broek",
                    Prijs = 14
                };

                Bestelling bestelling1 = new Bestelling
                {
                    Id = 1,
                    Klant = new Klant { Id = 1, Achternaam = "Trump", Voornaam = "Donald" },
                    Status = Status.NogTeKeuren,
                    BestelDatum = DateTime.Today
                };

                context.BestelArtikelSet.Add(new BestelArtikel
                {
                    Id = 1,
                    Aantal = 5,
                    Artikel = artikel1,
                    Bestelling = bestelling1
                });

                context.BestelArtikelSet.Add(new BestelArtikel
                {
                    Id = 2,
                    Aantal = 3,
                    Artikel = artikel2,
                    Bestelling = bestelling1
                });

                context.SaveChanges();

                Console.WriteLine(context.Artikelen.Count());

                var controller = new CommercieelManagerController(context);

                //Act
                var view = controller.Index();

                //Act
                var result = Assert.IsType<ViewResult>(view);
                var model = Assert.IsType<Models.CommercieelManagerModels.BestellingenViewModel>(result.Model);

                Assert.True(model.Bestellingen.Where(x => x.Id == 1).Single().Artikelen.Any());
            }
        }
    }
}
