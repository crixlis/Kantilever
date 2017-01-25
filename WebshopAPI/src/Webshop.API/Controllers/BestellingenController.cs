using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using rabbitmq_demo;
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
        public void Get()
        {
        }

        // GET api/bestellingen/5
        [HttpGet("{id}")]
        public void Get(int id)
        {
        }

        // POST api/bestellingen
        [HttpPost]
        public IActionResult Post([FromBody]Bestelling bestelling)
        {
            if (bestelling.Klant != null && bestelling.Artikelen.Count > 0)
            {
                    foreach (var artikel in bestelling.Artikelen)
                    {
                        if(artikel.Id <= 0) { throw new ArgumentException($"Artikel Id {artikel.Id} is ongeldig"); }
                    }

                var bestellingAanmaken = new BestellingAanmaken
                {
                    Artikelen = bestelling.Artikelen,
                    Klant = bestelling.Klant,
                    BestelDatum = DateTime.Now
                };

                _sender.PublishCommand(bestellingAanmaken);

                return CreatedAtRoute("api/bestellingen", bestelling);
            }

            return BadRequest("Er is iets fout gegaan met het toevoegen van het product. Controleer of de bestelling geldig is.");
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
