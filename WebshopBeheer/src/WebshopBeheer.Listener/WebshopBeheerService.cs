using MagazijnMedewerker.Database;
using CommercieelManager.Database;
using rabbitmq_demo;
using System;
using System.Collections.Generic;
using WebshopBeheer.Database;
using System.Linq;

namespace WebshopBeheer.Listener
{
    public class WebshopBeheerService : 
        IReceive<BestellingAangemaakt>,
        IReceive<BestellingGoedgekeurd>,  
        IReceive<FactuurAangemaakt>, 
        IReceive<BetaaldeFactuurAfgemeld>
    {
        private WebshopBeheerContext _webshopbeheerContext;
        private CommercieelManagerContext _commercieelManagerContext;
        private MagazijnMedewerkerContext _magazijnMedewerkerContext;
        private ISender _sender;

        public WebshopBeheerService(ISender sender)
        {
            _sender = sender;
        }

        public WebshopBeheerService(ISender sender, WebshopBeheerContext context)
        {
            _sender = sender;
            _webshopbeheerContext = context;

            _webshopbeheerContext.Database.EnsureCreated();
        }

        public void Execute(ArtikelAanCatalogusToegevoegd item)
        {
            var artikelBestaatAl = _magazijnMedewerkerContext.Artikelen.Any(x => x.Id == item.Id);
            if (artikelBestaatAl)
            {
                throw new ArgumentException($"Artikel met id {item.Id} bestaat al.");
            }

            var artikel = new MagazijnMedewerker.Database.Artikel
            {
                Id = item.Id,
                Beschrijving = item.Beschrijving,
                Leverancier = item.LeverancierCode,
                LeverbaarTot = item.LeverbaarTot,
                LeverbaarVanaf = item.LeverbaarVanaf,
                Categorieen = item.Categorieen,
                Naam = item.Naam,
                Prijs = item.Prijs,
                Voorraad = 0
            };
            _magazijnMedewerkerContext.Artikelen.Add(artikel);
            _magazijnMedewerkerContext.SaveChanges();
        }

        public void Execute(ArtikelInMagazijnGezet item)
        {
            var artikel = _magazijnMedewerkerContext.Artikelen.Single(x => x.Id == item.Id); //ArtikelId in catalogus hetzelfde als in magazijn?

            artikel.Voorraad += item.Hoeveelheid; // krijgen we de som of moeten we zelf optellen?
            _magazijnMedewerkerContext.Artikelen.Update(artikel);
            _magazijnMedewerkerContext.SaveChanges();
        }

        public void Execute(BestellingGoedgekeurd item)
        {
            var bestellingVoorCommercieelManager = _commercieelManagerContext.Bestellingen.Single(x => x.Id == item.Id);
            bestellingVoorCommercieelManager.Status = CommercieelManager.Database.Status.GoedGekeurd;
            _commercieelManagerContext.Update(bestellingVoorCommercieelManager);
            _commercieelManagerContext.SaveChanges();

            var bestellingVoorMagazijnMedewerker = _magazijnMedewerkerContext.Bestellingen.Single(x => x.Id == item.Id);
            bestellingVoorMagazijnMedewerker.Status = MagazijnMedewerker.Database.Status.GoedGekeurd;
            _magazijnMedewerkerContext.Update(bestellingVoorMagazijnMedewerker);
            _magazijnMedewerkerContext.SaveChanges();
        }

        public void Execute(FactuurAangemaakt item)
        {
            _sender.PublishCommand(new BetaaldeFactuurAfmelden { Id = item.Id });
        }

        public void Execute(BetaaldeFactuurAfgemeld item)
        {
            throw new NotImplementedException(message: "De cirkel is rond !!!");
        }

        public void Execute(BestellingAangemaakt item)
        {
            var bestellingVoorCommercieelManager = new CommercieelManager.Database.Bestelling
            {
                Id = item.Id,
                BestelDatum = item.BestelDatum,
                Status = CommercieelManager.Database.Status.NogTeKeuren
            };
            _commercieelManagerContext.Bestellingen.Add(bestellingVoorCommercieelManager);
            _commercieelManagerContext.SaveChanges();
            var lastBestellingVoorCommercieelManager = _commercieelManagerContext.Bestellingen.Last();

            foreach(var artikel in item.Artikelen)
            {
                var bestelArtikel = new CommercieelManager.Database.BestelArtikel
                {
                    Bestelling = lastBestellingVoorCommercieelManager,
                    Aantal = artikel.Voorraad, //Aantal
                    Artikel = _commercieelManagerContext.Artikelen.First(x => x.Id == item.Id)
                };
            }


            var bestellingVoorMagazijnMedewerker = new MagazijnMedewerker.Database.Bestelling
            {
                Id = item.Id,
                BestelDatum = item.BestelDatum,
                Klant = new MagazijnMedewerker.Database.Klant
                {
                    Id = item.Klant.Id,
                    Achternaam = item.Klant.Achternaam,
                    Adres = item.Klant.Adres,
                    Plaatsnaam = item.Klant.Plaatsnaam,
                    Postcode = item.Klant.Postcode,
                    Telefoonnummer = item.Klant.Telefoonnummer,
                    Voornaam = item.Klant.Voornaam
                },
                Status = MagazijnMedewerker.Database.Status.NogTeKeuren
            };
            _magazijnMedewerkerContext.Bestellingen.Add(bestellingVoorMagazijnMedewerker);
            _magazijnMedewerkerContext.SaveChanges();
        }
    }
}
