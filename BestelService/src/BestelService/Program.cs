using Autofac;
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

            using (var container = builder.Build())
            using (var listener = new rabbitmq_demo.Listener(connection, "Kantilever"))
            {
                listener.SubscribeCommands<BestellingKeuren>(container);
                listener.SubscribeCommands<BestellingAanmaken>(container);
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
