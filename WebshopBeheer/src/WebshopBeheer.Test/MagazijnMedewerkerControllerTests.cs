using System.Net;
using Xunit;
using WebshopBeheer.Controllers;

namespace WebshopBeheer.Test
{
    public class MagazijnMedewerkerControllerTests
    {
        [Fact]
        public async void TestOfPaginaBereikbaarIsNaHetStarten()
        {
            using (var server = CustomTestServer.Start())
            using (var client = server.CreateClient())
            {
                var controllerName = (new MagazijnMedewerkerController().GetType().Name).Replace("Controller", "");
                var status = await client.GetAsync(controllerName + "\\" + "Index");
                Assert.Equal(HttpStatusCode.OK, status.StatusCode);
            }

        }
    }
}
