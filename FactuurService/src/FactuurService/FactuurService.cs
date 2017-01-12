using rabbitmq_demo;

namespace FactuurService
{
    public class FactuurService : IReceive<FactuurAanmaken>
    {
        public FactuurService()
        {
        }

        public void Execute(FactuurAanmaken item)
        {
        }
    }
}
