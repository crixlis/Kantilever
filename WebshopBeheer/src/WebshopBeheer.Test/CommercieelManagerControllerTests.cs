using System.Net;
using WebshopBeheer.Controllers;
using Xunit;

namespace WebshopBeheer.Test
{
    public class CommercieelManagerControllerTests
    {
        [Fact]
        public async void TestOfPaginaBereikbaarIsNaHetStarten()
        {
            using (var server = CustomTestServer.Start())
            using (var client = server.CreateClient())
            {
                var controllerName = nameof(CommercieelManagerController).Replace("Controller","");
                var status = await client.GetAsync(controllerName + "\\" + "Index");
                Assert.Equal(HttpStatusCode.OK, status.StatusCode);
            }

        }
    }
}
