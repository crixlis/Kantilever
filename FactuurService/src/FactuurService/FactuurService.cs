using rabbitmq_demo;

namespace FactuurService
{
    public class FactuurService : IReceive<FactuurAanmaken>
    {
        private ISender _sender;

        public FactuurService()
        {
        }

        public FactuurService(ISender sender)
        {
            _sender = sender;
        }

        public void Execute(FactuurAanmaken item)
        {
            var factuurAangemaakt = new FactuurAangemaakt
            {
                ID = item.ID
            };

            _sender.PublishEvent(factuurAangemaakt);
        }
    }
}
