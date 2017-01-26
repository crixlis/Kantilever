using Autofac;
using BestelService.Database;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;
using RabbitMQ.Client;
using rabbitmq_demo;
using System;
using System.Threading;

namespace BestelService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "BestelService";
            Console.WriteLine("BestelService wacht op een inkomend bericht...");

            var connection = new ConnectionFactory()
                .FromEnvironment();

            var builder = new ContainerBuilder();
            builder.RegisterReceiverFor<BestelService, BestellingAanmaken>();
            builder.RegisterReceiverFor<BestelService, BestellingKeuren>();
            builder.Register(d => new Sender(connection, "Kantilever")).As<ISender>();

            var options = new DbContextOptionsBuilder<BestelServiceContext>()
               .UseMySQL(Environment.GetEnvironmentVariable("MYSQL_CONNECTION"))
               .Options;

            builder.RegisterType<BestelServiceContext>().As<IBestelServiceContext>();
            builder.RegisterInstance(options);

            using (var context = new BestelServiceContext(options))
            {
                context.Database.Migrate();
            }

            using (var container = builder.Build())
            using (var listener = new Listener(connection, "Kantilever"))
            {
                listener.SubscribeCommands<BestellingAanmaken>(container);
                listener.SubscribeCommands <BestellingKeuren>(container);
                listener.Received += ListenerMessage;
                using (ManualResetEvent manualResetEvent = new ManualResetEvent(false))
                {
                    manualResetEvent.WaitOne();
                }
            }
        }
        private static void ListenerMessage(object sender, ReceivedEventArgs e)
        {
            Console.WriteLine(e);
        }
    }
}
