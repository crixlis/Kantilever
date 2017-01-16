using rabbitmq_demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopBeheer.Listener
{
    public class WebshopBeheerService : IReceive<BestellingGoedgekeurd>, IReceive<FactuurAangemaakt>, IReceive<BetaaldeFactuurAfgemeld>
    {
        ISender _sender;

        public WebshopBeheerService(ISender sender)
        {
            _sender = sender;
        }

        public void Execute(BestellingGoedgekeurd item)
        {
            _sender.PublishCommand(new FactuurAanmaken { Id = item.Id });   
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
