using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using rabbitmq_demo;

using Webshop.Database;
using DatabaseArtikel = Webshop.Database.Artikel;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Webshop.API.Controllers
{
    [Route("api/[controller]")]
    public class ArtikelController : Controller
    {

        private ISender _sender;
        private IWebshopContext _context;

        public ArtikelController(ISender sender, IWebshopContext context)
        {
            _sender = sender;
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public List<DatabaseArtikel> Get()
        {
            return new List<DatabaseArtikel> {
                new DatabaseArtikel
                {
                    Id = 0,
                    Naam = "Giant XTC",
                    Beschrijving = "Mountainbike",
                    Prijs = 1000.99m,
                    LeverbaarVanaf = new DateTime(2017, 1, 1),
                    LeverbaarTot = new DateTime(2020, 1, 1),
                    Leverancier = "Giant",
                    Categorieen = new List<string> { "Mountainbikes", "Fietsen" }
                }
            };
           //return _context.Artikelen.ToList();          
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Artikel Get(int id)
        {
            var artikel1 = new Artikel
            {
                Id = 0,
                Naam = "Giant XTC",
                Beschrijving = "Mountainbike",
                Prijs = 1000.99m,
                LeverbaarVanaf = new DateTime(2017, 1, 1),
                LeverbaarTot = new DateTime(2020, 1, 1),
                Leverancier = "Giant",
                Categorieen = new List<string> { "Mountainbikes", "Fietsen" }
            };

            var artikel2 = new Artikel
            {
                Id = 1,
                Naam = "Giant Talon 2",
                Beschrijving = "Mountainbike",
                Prijs = 600.29m,
                LeverbaarVanaf = new DateTime(2017, 1, 1),
                LeverbaarTot = new DateTime(2020, 1, 1),
                Leverancier = "Giant",
                Categorieen = new List<string> { "Mountainbikes", "Fietsen" }
            };

            if (id == 0)
            {
                return artikel1;
            }else
            {
                return artikel2;
            }
        }
    }
}
