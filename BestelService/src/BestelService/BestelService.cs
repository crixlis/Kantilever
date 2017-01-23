using System;
using rabbitmq_demo;

namespace BestelService
{
    public class BestelService: IReceive<BestellingAanmaken>, IReceive<BestellingKeuren>, IReceive<BestellingGoedgekeurd>
    {
        private ISender _sender;
        private IBestelServiceContext _context;

        public BestelService(ISender sender, IBestelServiceContext context)
        {
            _sender = sender;
            _context = context;
        }

        public void Execute(BestellingAanmaken item)
        {
            _sender.PublishEvent(new BestellingAangemaakt{Id = item.Id });
        }

        public void Execute(BestellingKeuren item)
        {
            _context.Bestelling.Add(new Bestelling { Artikelen = item.Artikelen, Id = item.Id, Klant = item.Klant });
            _context.SaveChanges();
        }

        public void Execute(BestellingGoedgekeurd item)
        {
            _context.Bestelling.Add(new Bestelling { Artikelen = item.Artikelen, Id = item.Id, Klant = item.Klant });
            _context.SaveChanges();
        }
    }
}
