using System;
using rabbitmq_demo;

namespace FactuurService
{
    public class FactuurService : IReceive<BetaaldeFactuurAfmelden>, IReceive<FactuurAanmaken>
    {
        private ISender _sender;

        public FactuurService(ISender sender)
        {
            _sender = sender;
        }

        public void Execute(FactuurAanmaken item)
        {
            var newEvent = new FactuurAangemaakt
            {
                Id = item.Id
            };

            _sender.PublishEvent(newEvent);
        }

        public void Execute(BetaaldeFactuurAfmelden item)
        {
            var newEvent = new BetaaldeFactuurAfgemeld
            {
                Id = item.Id
            };

            _sender.PublishEvent(newEvent);
        }

    }
}
