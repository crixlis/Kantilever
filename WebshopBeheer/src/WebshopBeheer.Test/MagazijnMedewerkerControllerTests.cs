using System.Net;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace WebshopBeheer.Test
{
    public class MagazijnMedewerkerControllerTests
    {
        [Fact]
        public async void TestOfPaginaBereikbaarIsNaHetStarten()
        {
            using (var server = StartTestServer())
            using (var client = server.CreateClient())
            {
                var result = await client.GetAsync("MagazijnMedewerker/Index");
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            }

        }

        private static TestServer StartTestServer()
        {
            return new TestServer(new WebHostBuilder()
                            .UseStartup<Startup>()
                            .UseContentRoot(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "src", "WebshopBeheer")));
        }
    }
}
