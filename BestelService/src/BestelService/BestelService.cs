using rabbitmq_demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestelService
{
    public class BestelService: IReceive<BestellingAanmaken>, IReceive<BestellingKeuren>
    {
        ISender _sender;

        public BestelService(ISender sender)
        {
            _sender = sender;
        }

        public void Execute(BestellingAanmaken item)
        {
            _sender.PublishEvent(new BestellingAangemaakt{Id = item.Id });
        }

        public void Execute(BestellingKeuren item)
        {
            _sender.PublishEvent(new BestellingGoedGekeurd {Id = item.Id });
        }
    }
}
