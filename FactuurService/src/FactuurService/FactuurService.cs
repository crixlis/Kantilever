using System;
using rabbitmq_demo;
using FactuurService.Database;
using System.Linq;

namespace FactuurService
{
    public class FactuurService : IReceive<BetaaldeFactuurAfmelden>, IReceive<FactuurAanmaken>, IReceive<BestellingAangemaakt>
    {
        private ISender _sender;
        private IFactuurServiceContext _context;

        public FactuurService(ISender sender, IFactuurServiceContext context)
        {
            _sender = sender;
            _context = context;
        }

        public void Execute(FactuurAanmaken item)
        {
            var factuurInformatie = _context.Bestellingen.Where(b => b.Id == item.Id).Single();

            decimal totaal = 0;

            foreach (Artikel artikel in factuurInformatie.Artikelen)
            {
                totaal += artikel.Prijs;
            }

            Factuur factuur = new Factuur
            {
                Id = factuurInformatie.Id,
                Artikelen = factuurInformatie.Artikelen,
                Klant = factuurInformatie.Klant,
                HuidigeDatum = DateTime.Today,
                Totaal = totaal
            };
            _context.Facturen.Add(factuur);
            _context.SaveChanges();

            _sender.PublishEvent(new FactuurAangemaakt {
                Id = factuur.Id,
                Artikelen = factuur.Artikelen,
                HuidigeDatum = factuur.HuidigeDatum,
                Klant = factuur.Klant, Totaal = 
                factuur.Totaal });
        }

        public void Execute(BetaaldeFactuurAfmelden item)
        {
            var newEvent = new BetaaldeFactuurAfgemeld
            {
                Id = item.Id
            };
 
            _sender.PublishEvent(newEvent);
        }

        public void Execute(BestellingAangemaakt item)
        {
            _context.Bestellingen.Add(new Bestelling
            {
                Id = item.Id,
                Artikelen = item.Artikelen,
                Klant = item.Klant
            });

            _context.SaveChanges();        
        }
    }
}
