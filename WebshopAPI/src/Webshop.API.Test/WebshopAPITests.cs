using Xunit;
using Webshop.API;
using Webshop.API.Controllers;

namespace Webshop.API.Test
{
    public class WebshopAPITests
    {
        [Fact]
        public void ErKanEenBestellingAangemaaktWorden()
        {
            var bestelling = new Bestelling
            {
                ID = 0,
                Price = 22.30
            };

            var controller = new BestellingenController();
            controller.Post(bestelling);
        }
    }
}
