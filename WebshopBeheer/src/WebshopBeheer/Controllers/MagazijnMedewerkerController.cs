using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MagazijnMedewerker.Database;
using Microsoft.EntityFrameworkCore;
using viewModel = WebshopBeheer.Models.MagazijnMedewerkerModels.BestellingViewModel;
using viewModelArtikel = WebshopBeheer.Models.MagazijnMedewerkerModels.Artikel;

namespace WebshopBeheer.Controllers
{
    public class MagazijnMedewerkerController : Controller
    {
        private MagazijnMedewerkerContext _context;

        public MagazijnMedewerkerController(MagazijnMedewerkerContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "Magazijnmedewerker";

            var bestelling = _context
                .Bestellingen
                .Where(b => b.Status == Status.GoedGekeurd)
                .OrderBy(b => b.BestelDatum)
                .FirstOrDefault();

            if(bestelling == null)
            {
                return View(null);
            }

            var artikelen = _context.BestelArtikelSet
                .Where(x => x.Bestelling.Id == bestelling.Id)
                .Include(x => x.Artikel)
                .Select(x => new viewModelArtikel {
                    Id = x.Artikel.Id,
                    Aantal = x.Aantal,
                    Categorieen = x.Artikel.Categorieen,
                    Naam = x.Artikel.Naam
                });

            return View(new viewModel {
                Id = bestelling.Id,
                Artikelen = artikelen,
                BestelDatum = bestelling.BestelDatum,
                Klant = bestelling.Klant,
                Status = bestelling.Status
            });
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
