using System.Net;
using System.Net.Http;
using Xunit;

namespace WebshopBeheer.Test
{
    public class MagazijnMedewerkerControllerTests
    {
        [Fact]
        public async void TestOfPaginaBereikbaarIsNaHetStarten()
        {
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.GetAsync("Http://localhost:15282/MagazijnMedewerker/Index");
                var statuscode = result.StatusCode;

                Assert.Equal(HttpStatusCode.OK, statuscode);
            }
        }
    }
}
