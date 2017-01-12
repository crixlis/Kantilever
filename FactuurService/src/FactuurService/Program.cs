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
            Console.Title = "FactuurService";
            Console.WriteLine("De factuurservice wacht op een nieuwe factuur die door het magazijn wordt opgegooid...");

            var connection = new ConnectionFactory { HostName = "cursistm07", UserName = "manuel", Password = "manuel" };
            var builder = new ContainerBuilder();
            builder.RegisterReceiverFor<FactuurService, FactuurAanmaken>();

            using (var container = builder.Build())
            using (var listener = new Listener(connection, "Kantilever"))
            {
                listener.SubscribeCommands<FactuurAanmaken>(container);
                listener.Received += Listener_Received;
                using (var mEvent = new ManualResetEvent(false)){ 
                    mEvent.WaitOne();
                }
            }
        }

        private static void Listener_Received(object sender, ReceivedEventArgs e)
        {
            Console.WriteLine($"Factuur ontvangen: {e}");
        }
    }
}
