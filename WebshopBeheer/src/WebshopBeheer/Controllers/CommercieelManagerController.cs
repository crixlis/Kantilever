using Microsoft.AspNetCore.Mvc;
using CommercieelManager.Database;
using ViewModel = WebshopBeheer.Models.CommercieelManagerModels.BestellingenViewModel;
using ViewModelBestelling = WebshopBeheer.Models.CommercieelManagerModels.Bestelling;
using ViewModelArtikel = WebshopBeheer.Models.CommercieelManagerModels.Artikel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using rabbitmq_demo;

namespace WebshopBeheer.Controllers
{
    public class CommercieelManagerController : Controller
    {
        private CommercieelManagerContext _context;
        private ISender _sender;

        public CommercieelManagerController(CommercieelManagerContext context, ISender sender)
        {
            _context = context;
            _sender = sender;
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
                return View(new ViewModel
                {
                    Bestellingen = Enumerable.Empty<ViewModelBestelling>()
                });
            }

            var viewModel = new ViewModel
            {
                Bestellingen = bestellingen.Select(x => new ViewModelBestelling
                {
                    Id = x.Id,
                    BestelDatum = x.BestelDatum,
                    Klant = x.Klant
                }).ToArray()
            };

            foreach (ViewModelBestelling bestelling in viewModel.Bestellingen)
            {
                bestelling.Artikelen = _context.BestelArtikelSet
                .Where(x => x.Bestelling.Id == bestelling.Id)
                .Include(x => x.Artikel)
                .Select(x => new ViewModelArtikel
                {
                    Id = x.Artikel.Id,
                    Aantal = x.Aantal,
                    Prijs = x.Artikel.Prijs,
                    Naam = x.Artikel.Naam
                }).ToArray();
            }

            return View(viewModel);
        }

        public IActionResult Error()
        {
            return View();
        }

        public void KeurBestellingGoed(int id)
        {
            _sender.PublishCommand(new Controllers.Commands.BestellingKeuren
            {
                Id = id,
                Status = Commands.Status.GoedGekeurd
            });
        }

        public void KeurBestellingAf(int id)
        {
            _sender.PublishCommand(new Commands.BestellingKeuren
            {
                Id = id,
                Status = Commands.Status.Afgekeurd
            });
        }

    }
}
