using System;
using rabbitmq_demo;
using System.Linq;

namespace BestelService
{
    public class BestelService: IReceive<BestellingAanmaken>, IReceive<BestellingGoedgekeurd>
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
            _context.Bestelling.Add(new Bestelling { Artikelen = item.Artikelen, Id = item.Id, Klant = item.Klant });
            _context.SaveChanges();
            _sender.PublishEvent(new BestellingKeuren{Id = item.Id });
        }
        public void Execute(BestellingGoedgekeurd item)
        {
            var goedgekeurdeBestelling = _context.Bestelling.Where(b => b.Id == item.Id).Single();
            _context.Bestelling.Update(goedgekeurdeBestelling);
            _context.SaveChanges();
        }
    }
}
