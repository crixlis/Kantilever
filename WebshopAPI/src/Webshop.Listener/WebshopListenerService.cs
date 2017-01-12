using rabbitmq_demo;

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

        }
    }
}