using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebshopBeheer.Database;
using Microsoft.EntityFrameworkCore;

namespace WebshopBeheer.Controllers
{
    public class MagazijnMedewerkerController : Controller
    {
        private WebshopBeheerContext _context;

        public MagazijnMedewerkerController(WebshopBeheerContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "Magazijnmedewerker";


            //_context.Add(new Bestelling() { Id = 1, Klant = new Klant() { Id = 1, Achternaam = "Slager" }, Status = 0, BestelDatum = DateTime.Now, Artikelen = new List<Artikel>() { new Artikel() { ArtikelId = 1, Naam = "Fietsband" } } });
            //_context.SaveChanges();

            var bestelling = _context
                .Bestellingen
                .Include(b => b.Klant)
                .Include(b => b.Artikelen)
                .Where(b => b.Status == 0)
                .OrderBy(b => b.BestelDatum)
                .FirstOrDefault();


            return View(bestelling);
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
