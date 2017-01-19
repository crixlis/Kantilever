using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rabbitmq_demo;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Webshop.API.Controllers
{
    [Route("api/[controller]")]
    public class ArtikelController : Controller
    {

        private ISender _sender;

        public ArtikelController(ISender sender)
        {
            _sender = sender;
        }

        // GET: api/values
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

        // GET api/values/5
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
                Categorieen = new string[] { "Mountainbikes", "Fietsen" }
            };
        }
    }
}
