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
        private IWebshopContext _context;

        public BestellingenController(ISender sender, IWebshopContext context)
        {
            _sender = sender;
            _context = cont
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
            if (bestelling.Id != 0 && bestelling.Klant != null && bestelling.Artikelen.Count > 0)
            {
                var bestellingKeuren = new BestellingKeuren
                {
                    Id = bestelling.Id,
                    Artikelen = bestelling.Artikelen,
                    Klant = bestelling.Klant
                };

                _sender.PublishCommand(bestellingKeuren);

                return CreatedAtRoute("api/bestellingen", bestelling);
            }

            return BadRequest("Er is iets fout gegaan met het toevoegen van het product. Controleer of de bestelling compleet is.");
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
