using rabbitmq_demo;
using System;

namespace Webshop.Listener
{
    public class WebshopListenerService : IReceive<BetaaldeFactuurAfgemeld>
    {
        private ISender _sender;

        public WebshopListenerService(ISender sender)
        {
           _sender = sender;
        }

        public void Execute(BetaaldeFactuurAfgemeld item)
        {
            throw new NotImplementedException();
        }
    }
}