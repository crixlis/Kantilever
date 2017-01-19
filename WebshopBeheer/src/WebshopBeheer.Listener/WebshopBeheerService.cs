using rabbitmq_demo;
using System;
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
            var bestelling = new Bestelling
            {
                Id = item.Id
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
