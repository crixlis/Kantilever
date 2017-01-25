using System;
using rabbitmq_demo;
using System.Linq;
using BestelService.Database;

namespace BestelService
{
    public class BestelService: IReceive<BestellingAanmaken>, IReceive<BestellingKeuren>
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
            //Patrick, niet aankomen s.v.p.
            _context.Bestelling.Add(new Bestelling { Artikelen = item.Artikelen, Klant = item.Klant });
            _context.SaveChanges();
            var last = _context.Bestelling.Last();
            _sender.PublishEvent(new BestellingAangemaakt { Id = last.Id, Artikelen = last.Artikelen, Klant = last.Klant, BestelDatum = item.BestelDatum });
        }

        public void Execute(BestellingKeuren item)
        {
            var teKeurenBestelling = _context.Bestelling.Where(b => b.Id == item.Id).Single();
            teKeurenBestelling.Status = item.Status;
            _context.Bestelling.Update(teKeurenBestelling);
            _context.SaveChanges();
            var last = _context.Bestelling.Last();
            if(item.Status == Status.GoedGekeurd)
            {
                _sender.PublishEvent(new BestellingGoedgekeurd
                {
                    Id = last.Id
                });
            } else
            {
                _sender.PublishEvent(new BestellingAfgekeurd
                {
                    Id = last.Id
                });
            }
           
        }
    }
}
