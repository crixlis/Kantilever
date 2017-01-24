using System;
using rabbitmq_demo;
using FactuurService.Database;

namespace FactuurService
{
    public class FactuurService : IReceive<BetaaldeFactuurAfmelden>, IReceive<FactuurAanmaken>
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
            decimal totaal = 0;

            foreach (Artikel artikel in item.Artikelen)
            {
                totaal += artikel.Prijs;
            }

            _sender.PublishEvent(new FactuurAangemaakt
            {
                Id = item.Id,
                Artikelen = item.Artikelen,
                Klant = item.Klant,
                BetalenVoorDatum = DateTime.Today.AddMonths(1),
                Totaal = totaal
            });
        }

        public void Execute(BetaaldeFactuurAfmelden item)
        {
            var newEvent = new BetaaldeFactuurAfgemeld
            {
                Id = item.Id
            };

            _sender.PublishEvent(newEvent);
        }

    }
}
