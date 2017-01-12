using rabbitmq_demo;

namespace FactuurService
{
    public class FactuurService : IReceive<FactuurAanmaken>
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
                ID = item.ID
            };

            _sender.PublishEvent(newEvent);
        }
    }
}
