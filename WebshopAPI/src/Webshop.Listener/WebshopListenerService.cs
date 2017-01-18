using rabbitmq_demo;
using System;

namespace Webshop.Listener
{
    public class WebshopListenerService : IReceive<BetaaldeFactuurAfgemeld>, IReceive<ArtikelAanCatalogusToegevoegd>
    {
        private ISender _sender;

        public WebshopListenerService(ISender sender)
        {
           _sender = sender;
        }

        public void Execute(ArtikelAanCatalogusToegevoegd item)
        {
            throw new NotImplementedException();
        }

        public void Execute(BetaaldeFactuurAfgemeld item)
        {
            throw new NotImplementedException();
        }
    }
}