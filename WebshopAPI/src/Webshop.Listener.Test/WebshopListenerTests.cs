using NSubstitute;
using rabbitmq_demo;
using Xunit;

namespace Webshop.Listener.Test
{
    public class WebshopListenerTests
    {
        [Fact]
        public void DeWebshopListenerKanEenBetaaldeFactuurAfgemeldEventOntvangen()
        {
            //Arrange
            var sender = Substitute.For<ISender>();
            var service = new WebshopListenerService(sender);

            var factuurAfgemeld = new BetaaldeFactuurAfgemeld
            {
                ID = 0
            };

            //Act + Asser ... er wordt tot nu toe alleen gecontrolleerd of een BetaaldeFactuurAfgemeld event ontvangen kan worden
            service.Execute(factuurAfgemeld);
        }
    }
}
