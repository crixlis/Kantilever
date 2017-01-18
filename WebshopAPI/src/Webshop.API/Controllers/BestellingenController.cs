using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using rabbitmq_demo;
using RabbitMQ.Client;
using System;

namespace Webshop.API.Controllers
{
    [Route("api/[controller]")]
    public class BestellingenController : Controller
    {
        private ISender _sender;

        public BestellingenController(ISender sender)
        {
            _sender = sender;
        }

        // GET api/bestellingen
        [HttpGet]
        public List<Artikel> Get()
        {
            return new List<Artikel>
            {
                new Artikel
                {
                    Id = 0,
                    Naam = "Giant XTC",
                    Beschrijving = "Mountainbike",
                    Prijs = 1000.99m,
                    LeverbaarVanaf = new DateTime(2017, 1, 1),
                    LeverbaarTot = new DateTime(2020, 1, 1),
                    Leverancier = "Giant",
                    Categorieen = new string[]{ "Mountainbikes", "Fietsen" }
                },
                new Artikel
                {
                    Id = 1,
                    Naam = "Giant Talon 2",
                    Beschrijving = "Mountainbike",
                    Prijs = 600.29m,
                    LeverbaarVanaf = new DateTime(2017, 1, 1),
                    LeverbaarTot = new DateTime(2020, 1, 1),
                    Leverancier = "Giant",
                    Categorieen = new string[]{ "Mountainbikes", "Fietsen" }
                }
            };
        }

        // GET api/bestellingen/5
        [HttpGet("{id}")]
        public Artikel Get(int id)
        {
            return new Artikel
            {
                Id = 0,
                Naam = "Giant XTC",
                Beschrijving = "Mountainbike",
                Prijs = 1000.99m,
                LeverbaarVanaf = new DateTime(2017, 1, 1),
                LeverbaarTot = new DateTime(2020, 1, 1),
                Leverancier = "Giant",
                Categorieen = new string[]{ "Mountainbikes", "Fietsen" }
            };
        }

        // POST api/bestellingen
        [HttpPost]
        public void Post([FromBody]Bestelling bestelling)
        {
            if (bestelling.Id != 0 && bestelling.Klant != null && bestelling.Artikelen != null) {
                var bestellingKeuren = new BestellingKeuren
                {
                    Id = bestelling.Id,
                    Artikelen = bestelling.Artikelen,
                    Klant = bestelling.Klant
                };

                _sender.PublishCommand(bestellingKeuren);

                Ok();
            }

        }

        // PUT api/bestellingen/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/bestellingen/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
