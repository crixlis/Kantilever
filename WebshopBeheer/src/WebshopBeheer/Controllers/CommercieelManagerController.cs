using Microsoft.AspNetCore.Mvc;
using CommercieelManager.Database;
using viewModel = WebshopBeheer.Models.CommercieelManagerModels.BestellingViewModel;
using viewModelArtikel = WebshopBeheer.Models.CommercieelManagerModels.Artikel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebshopBeheer.Controllers
{
    public class CommercieelManagerController : Controller
    {
        private CommercieelManagerContext _context;

        public CommercieelManagerController(CommercieelManagerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "CommercieelManager";

            var bestelling = _context
                .Bestellingen
                .Where(b => b.Status == Status.NogTeKeuren)
                .OrderBy(b => b.BestelDatum)
                .FirstOrDefault();

            if (bestelling == null)
            {
                return View(null);
            }

            var artikelen = _context.BestelArtikelSet
                .Where(x => x.Bestelling.Id == bestelling.Id)
                .Include(x => x.Artikel)
                .Select(x => new viewModelArtikel
                {
                    Id = x.Artikel.Id,
                    Aantal = x.Aantal,
                    Prijs = x.Artikel.Prijs,
                    Naam = x.Artikel.Naam
                });

            return View(new viewModel
            {
                Id = bestelling.Id,
                Artikelen = artikelen,
                BestelDatum = bestelling.BestelDatum,
                Klant = bestelling.Klant
            });
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
