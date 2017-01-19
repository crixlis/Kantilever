using System.Net;
using WebshopBeheer.Controllers;
using Xunit;

namespace WebshopBeheer.Test
{
    public class SalesManagerControllerTests
    {
        [Fact]
        public async void TestOfPaginaBereikbaarIsNaHetStarten()
        {
            using (var server = CustomTestServer.Start())
            using (var client = server.CreateClient())
            {
                var controllerName = (new SalesManagerController().GetType().Name).Replace("Controller", "");
                var status = await client.GetAsync(controllerName + "\\" + "Index");
                Assert.Equal(HttpStatusCode.OK, status.StatusCode);
            }
        }
    }
}
