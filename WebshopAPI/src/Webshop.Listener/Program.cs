using Autofac;
using RabbitMQ.Client;
using rabbitmq_demo;
using System;
using System.Threading;

namespace Webshop.Listener
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Webshop.Listener";
            Console.WriteLine("De webshoplistener wacht op een nieuwe events...");

            var connection = new ConnectionFactory { HostName = "cursistm07", UserName = "manuel", Password = "manuel" };
            var builder = new ContainerBuilder();
            builder.RegisterReceiverFor<WebshopListenerService, BetaaldeFactuurAfgemeld>();

            using (var container = builder.Build())
            using (var listener = new rabbitmq_demo.Listener(connection, "Kantilever"))
            {
                listener.SubscribeEvents<BetaaldeFactuurAfgemeld>(container);
                listener.Received += Listener_Received;
                using (var mEvent = new ManualResetEvent(false))
                {
                    mEvent.WaitOne();
                }
            }
        }

        private static void Listener_Received(object sender, ReceivedEventArgs e)
        {
            Console.WriteLine($"Betaalde factuur afgemeld: {e}");
        }
    }
}
