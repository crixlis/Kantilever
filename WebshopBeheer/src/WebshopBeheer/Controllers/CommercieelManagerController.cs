using Microsoft.AspNetCore.Mvc;
using CommercieelManager.Database;
using ViewModel = WebshopBeheer.Models.CommercieelManagerModels.BestellingenViewModel;
using ViewModelBestelling = WebshopBeheer.Models.CommercieelManagerModels.Bestelling;
using ViewModelArtikel = WebshopBeheer.Models.CommercieelManagerModels.Artikel;
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

            var bestellingen = _context
                .Bestellingen
                .Where(b => b.Status == Status.NogTeKeuren)
                .OrderBy(b => b.BestelDatum);

            if (bestellingen.Count() == 0)
            {
                return View(new ViewModel {
                    Bestellingen = Enumerable.Empty<ViewModelBestelling>()
                });
            }

            var viewModel = new ViewModel
            {
                Bestellingen = bestellingen.Select(x => new ViewModelBestelling
                {
                    Id = x.Id,
                    BestelDatum = x.BestelDatum,
                    Klant = x.Klant,
                })
            };

            foreach(ViewModelBestelling bestelling in viewModel.Bestellingen)
            {
                bestelling.Artikelen = _context.BestelArtikelSet
                .Where(x => x.Id == bestelling.Id)
                .Include(x => x.Artikel)
                .Select(x => new ViewModelArtikel
                {
                    Id = x.Artikel.Id,
                    Aantal = x.Aantal,
                    Prijs = x.Artikel.Prijs,
                    Naam = x.Artikel.Naam
                });
            }

            return View(viewModel);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
