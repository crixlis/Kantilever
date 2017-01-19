using rabbitmq_demo;
using System;
using System.Collections.Generic;
using WebshopBeheer.Database;

namespace WebshopBeheer.Listener
{
    public class WebshopBeheerService : IReceive<BestellingKeuren>, IReceive<BestellingGoedgekeurd>, IReceive<FactuurAangemaakt>, IReceive<BetaaldeFactuurAfgemeld>
    {
        private WebshopBeheerContext _context;
        private ISender _sender;

        public WebshopBeheerService(ISender sender)
        {
            _sender = sender;
        }

        public WebshopBeheerService(ISender sender, WebshopBeheerContext context)
        {
            _sender = sender;
            _context = context;

            _context.Database.EnsureCreated();
        }

        public void Execute(BestellingGoedgekeurd item)
        {
            _sender.PublishCommand(new FactuurAanmaken { Id = item.Id });   
        }

        public void Execute(BestellingKeuren item)
        {
            List<Artikel> artikelen = new List<Artikel>();

            foreach (var artikel in item.Artikelen)
            {
                artikelen.Add(
                    new Artikel
                    {
                        Id = artikel.Id,
                        Beschrijving = artikel.Beschrijving,
                        Leverancier = artikel.Leverancier,
                        LeverbaarTot = artikel.LeverbaarTot,
                        LeverbaarVanaf = artikel.LeverbaarVanaf,
                        Naam = artikel.Naam,
                        Prijs = artikel.Prijs,
                        Categorieen = artikel.Categorieen,
                        Voorraad = artikel.Voorraad
                    }
                );
            }

            var bestelling = new Bestelling
            {
                Id = item.Id,
                Klant = new Klant
                {
                    Id = item.Klant.Id,
                    Achternaam = item.Klant.Achternaam,
                    Voornaam = item.Klant.Voornaam,
                    Adres = item.Klant.Adres,
                    Plaatsnaam = item.Klant.Plaatsnaam,
                    Postcode = item.Klant.Postcode,
                    Telefoonnummer = item.Klant.Telefoonnummer
                },
                Artikelen = artikelen
            };

            _context.Bestellingen.Add(bestelling);
            _context.SaveChanges();
        }

        public void Execute(FactuurAangemaakt item)
        {
            _sender.PublishCommand(new BetaaldeFactuurAfmelden { Id = item.Id });
        }

        public void Execute(BetaaldeFactuurAfgemeld item)
        {
            throw new NotImplementedException(message: "De cirkel is rond !!!");
        }
    }
}
