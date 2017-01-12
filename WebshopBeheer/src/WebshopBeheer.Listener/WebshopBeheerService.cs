using rabbitmq_demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopBeheer.Listener
{
    public class WebshopBeheerService : IReceive<bestellingGoedgekeurd>, IReceive<factuurAangemaakt>, IReceive<betaaldeFactuurAfgemeld>
    {
        ISender _sender;

        public WebshopBeheerService(ISender sender)
        {
            _sender = sender;
        }

        public void Execute(bestellingGoedgekeurd item)
        {
            _sender.PublishEvent(new factuurAanmaken { Id = 1 });   
        }
        public void Execute(factuurAangemaakt item)
        {
            _sender.PublishEvent(new betaaldeFactuurAfmelden { Id = 1 });
        }

        public void Execute(betaaldeFactuurAfgemeld item)
        {
            throw new NotImplementedException();
        }
    }
}
