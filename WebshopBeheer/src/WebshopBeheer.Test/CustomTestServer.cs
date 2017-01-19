using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.IO;

namespace WebshopBeheer.Test
{
    public static class CustomTestServer
    {
        public static TestServer Start()
        {
            var assemblyName = (typeof(Program).Namespace).Split('.')[0];

            return new TestServer(new WebHostBuilder()
                            .UseStartup<Startup>()
                            .UseContentRoot(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "src", assemblyName)));
        }
    }
}
