using rabbitmq_demo;

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
            _sender.PublishEvent(new BestellingGoedgekeurd {Id = item.Id });
        }
    }
}
