using rabbitmq_demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestelService
{
    public class BestelService: IReceive<bestellingAanmaken>, IReceive<bestellingKeuren>
    {
        ISender _sender;

        public BestelService(ISender sender)
        {
            _sender = sender;
        }

        public void Execute(bestellingAanmaken item)
        {
            _sender.PublishEvent(new bestellingAangemaakt{Id = item.Id });
        }

        public void Execute(bestellingKeuren item)
        {
            _sender.PublishEvent(new bestellingGekeurd {Id = item.Id });
        }
    }
}
