using Autofac;
using RabbitMQ.Client;
using rabbitmq_demo;
using System;
using System.Threading;

namespace FactuurService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "FactuurService.";
            Console.WriteLine("De factuurservice ontvangt nieuwe facturen die door het magazijn worden opgegooid.");

            var connection = new ConnectionFactory { HostName = "cursistm07", UserName = "manuel", Password = "manuel" };
            var builder = new ContainerBuilder();
            builder.RegisterReceiverFor<FactuurService, FactuurAanmaken>();

            using (var mEvent = new ManualResetEvent(false))
            using (var container = builder.Build())
            using (var listener = new Listener(connection, "Kantilever"))
            {
                listener.SubscribeCommands<FactuurAanmaken>(container);
                listener.Received += Listener_Received;
                mEvent.WaitOne();
            }
        }

        private static void Listener_Received(object sender, ReceivedEventArgs e)
        {
            Console.WriteLine($"Factuur ontvangen: {e}");
        }
    }
}
