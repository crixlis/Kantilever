using rabbitmq_demo;
using System;
using System.IO;
using System.Linq;
using Webshop.Database;

namespace Webshop.Listener
{
    public class WebshopListenerService : 
        IReceive<ArtikelAanCatalogusToegevoegd>, 
        IReceive<ArtikelVoorraadBijgewerkt>
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
            var artikel = _context.Artikelen.SingleOrDefault(x => x.Id == item.Id);

            if(artikel == null)
            {
                artikel = new Artikel
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

                if (item.Afbeelding != null && item.Afbeelding.Any())
                {
                    artikel.ImagePath = SaveImage(item.Afbeelding);
                }

                _context.Artikelen.Add(artikel);
            } else
            {
                artikel.Beschrijving = item.Beschrijving;
                artikel.Categorieen = item.Categorieen;
                artikel.ImagePath = item.ImagePath;
                artikel.Leverancier = item.Leverancier;
                artikel.LeverancierCode = item.LeverancierCode;
                artikel.LeverbaarTot = item.LeverbaarTot;
                artikel.LeverbaarVanaf = item.LeverbaarVanaf;
                artikel.Naam = item.Naam;
                artikel.Prijs = item.Prijs;

                var path = _context.Artikelen.Single(x => x.Id == item.Id).ImagePath;
                if (path != null)
                {
                    RemoveImage(path);
                }

                if (item.Afbeelding != null && item.Afbeelding.Any())
                {
                    artikel.ImagePath = SaveImage(item.Afbeelding);
                }
                _context.Artikelen.Update(artikel);
            }
            _context.SaveChanges();

        }

        private void RemoveImage(string path)
        {
            File.Delete(path);
        }

        private string SaveImage(byte[] img)
        {
            Guid imgName;

            imgName = Guid.NewGuid();
            string rootPath = Path.Combine(_imgRoot, imgName.ToString());

            File.WriteAllBytes(rootPath + ".png", img);
            return Path.Combine(rootPath, ".png");
        }
        
        public void Execute(ArtikelVoorraadBijgewerkt item)
        {

            var artikel = _context.Artikelen.Where(a => a.Id == item.Id).SingleOrDefault();
            if (artikel != null)
            {
                artikel.Voorraad = item.Voorraad;
                _context.Artikelen.Update(artikel);
                
            } else
            {
                var nieuwartikel = new Artikel
                {
                    Id = item.Id,
                    Voorraad = item.Voorraad
                };
                _context.Artikelen.Add(nieuwartikel);
            }
            _context.SaveChanges();
        }
    }
}