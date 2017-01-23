using rabbitmq_demo;
using System;
using System.IO;

namespace Webshop.Listener
{
    public class WebshopListenerService : IReceive<BetaaldeFactuurAfgemeld>, IReceive<ArtikelAanCatalogusToegevoegd>
    {
        private ISender _sender;
        private IWebshopContext _context;
        private string _imgRoot; 

        public WebshopListenerService(ISender sender, IWebshopContext context, string imgRoot)
        {
            _sender = sender;
            _context = context;
            _imgRoot = imgRoot;
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
            
            if(item.Afbeelding != null)
            {
                File.WriteAllBytes(Path.Combine(_imgRoot, item.Id.ToString()), item.Afbeelding);
            }

            _context.Artikelen.Add(nieuwArtikel);
            _context.SaveChanges();
        }

        public void Execute(BetaaldeFactuurAfgemeld item)
        {
            throw new NotImplementedException();
        }
    }
}