using rabbitmq_demo;
using System;

namespace Webshop.Listener
{
    public class WebshopListenerService : IReceive<BetaaldeFactuurAfgemeld>, IReceive<ArtikelAanCatalogusToegevoegd>
    {
        private ISender _sender;
        private IWebshopContext _context;

        public WebshopListenerService(ISender sender, IWebshopContext context)
        {
           _sender = sender;
           _context = context;
        }

        public void Execute(ArtikelAanCatalogusToegevoegd item)
        {
            var nieuwArtikel = new Artikel
            {
                Beschrijving = item.Beschrijving,
                Categorieen = item.Categorieen,
                Id = item.Id,
                Leverancier = item.Leverancier,
                LeverancierCode = item.LeverancierCode,
                LeverbaarTot = item.LeverbaarTot,
                LeverbaarVanaf = item.LeverbaarVanaf,
                Naam = item.Naam,
                Prijs = item.Prijs
            };

            _context.Artikelen.Add(nieuwArtikel);
            _context.SaveChanges();
        }

        public void Execute(BetaaldeFactuurAfgemeld item)
        {
            throw new NotImplementedException();
        }
    }
}