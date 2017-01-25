using MagazijnMedewerker.Database;
using CommercieelManager.Database;
using rabbitmq_demo;
using System;
using System.Linq;

namespace WebshopBeheer.Listener
{
    public class WebshopBeheerService : 
        IReceive<BestellingAangemaakt>,
        IReceive<BestellingGoedgekeurd>,  
        IReceive<FactuurAangemaakt>, 
        IReceive<BetaaldeFactuurAfgemeld>
    {
        private CommercieelManagerContext _commercieelManagerContext;
        private MagazijnMedewerkerContext _magazijnMedewerkerContext;
        private ISender _sender;

        public WebshopBeheerService(ISender sender)
        {
            _sender = sender;
        }

        public void Execute(ArtikelAanCatalogusToegevoegd item)
        {
            ArtikelAanCatalogusToegevoegdVoorCommercieelManager(item);
            ArtikelAanCatalogusToegevoegdVoorMagazijnMedewerker(item);
        }

        private void ArtikelAanCatalogusToegevoegdVoorCommercieelManager(ArtikelAanCatalogusToegevoegd item)
        {
            var artikel = _commercieelManagerContext.Artikelen.SingleOrDefault(x => x.Id == item.Id);

            if (artikel == null)
            {
                artikel = new CommercieelManager.Database.Artikel
                {
                    Id = item.Id,
                    Naam = item.Naam,
                    Prijs = item.Prijs
                };

                _commercieelManagerContext.Artikelen.Add(artikel);
            }
            else
            {
                artikel.Naam = item.Naam;
                artikel.Prijs = item.Prijs;
                _commercieelManagerContext.Artikelen.Update(artikel);
            }
            _commercieelManagerContext.SaveChanges();
        }

        private void ArtikelAanCatalogusToegevoegdVoorMagazijnMedewerker(ArtikelAanCatalogusToegevoegd item)
        {
            var artikel = _magazijnMedewerkerContext.Artikelen.SingleOrDefault(x => x.Id == item.Id);

            if (artikel == null)
            {
                artikel = new MagazijnMedewerker.Database.Artikel
                {
                    Beschrijving = item.Beschrijving,
                    Categorieen = item.Categorieen,
                    Leverancier = item.LeverancierCode,
                    Id = item.Id,
                    LeverbaarTot = item.LeverbaarTot,
                    LeverbaarVanaf = item.LeverbaarVanaf,
                    Naam = item.Naam,
                    Prijs = item.Prijs
                };

                _magazijnMedewerkerContext.Artikelen.Add(artikel);
            }
            else
            {
                artikel.Beschrijving = item.Beschrijving;
                artikel.Categorieen = item.Categorieen;
                artikel.Leverancier = item.LeverancierCode;
                artikel.LeverbaarTot = item.LeverbaarTot;
                artikel.LeverbaarVanaf = item.LeverbaarVanaf;
                artikel.Naam = item.Naam;
                artikel.Prijs = item.Prijs;
                _magazijnMedewerkerContext.Artikelen.Update(artikel);
            }
            _magazijnMedewerkerContext.SaveChanges();
        }

        public void Execute(ArtikelInMagazijnGezet item)
        {
            ArtikelInMagazijnGezetVoorMagazijnMedewerker(item);
        }

        private void ArtikelInMagazijnGezetVoorMagazijnMedewerker(ArtikelInMagazijnGezet item)
        {
            var artikel = _magazijnMedewerkerContext.Artikelen.Where(a => a.Id == item.Id).SingleOrDefault();
            if (artikel != null)
            {
                artikel.Voorraad = item.Hoeveelheid;
                _magazijnMedewerkerContext.Artikelen.Update(artikel);

            }
            else
            {
                var nieuwartikel = new MagazijnMedewerker.Database.Artikel
                {
                    Id = item.Id,
                    Voorraad = item.Hoeveelheid
                };
                _magazijnMedewerkerContext.Artikelen.Add(nieuwartikel);
            }
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
            BestellingAangemaaktVoorCommercieelManager(item);
            BestellingAangemaaktVoorMagazijnMedewerker(item);  
        }

        private void BestellingAangemaaktVoorCommercieelManager(BestellingAangemaakt item)
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

            foreach (var artikel in item.Artikelen)
            {
                var bestelArtikel = new CommercieelManager.Database.BestelArtikel
                {
                    Bestelling = lastBestellingVoorCommercieelManager,
                    Aantal = artikel.Aantal,
                    Artikel = _commercieelManagerContext.Artikelen.First(x => x.Id == item.Id)
                };
            }
        }

        private void BestellingAangemaaktVoorMagazijnMedewerker(BestellingAangemaakt item)
        {
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
