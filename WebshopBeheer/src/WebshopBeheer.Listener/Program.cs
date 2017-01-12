using Autofac;
using RabbitMQ.Client;
using rabbitmq_demo;
using System;
using System.Threading;

namespace WebshopBeheer.Listener
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "WebshopBeheer";
            Console.WriteLine("WebshopBeheer wacht op een inkomend bericht...");

            var connection = new ConnectionFactory { HostName = "cursistm07", UserName = "manuel", Password = "manuel" };

            var builder = new ContainerBuilder();
            builder.RegisterReceiverFor<WebshopBeheerService, bestellingGoedgekeurd>();
            builder.RegisterReceiverFor<WebshopBeheerService, factuurAangemaakt>();
            builder.RegisterReceiverFor<WebshopBeheerService, betaaldeFactuurAfgemeld>();

            using (var container = builder.Build())
            using (var listener = new rabbitmq_demo.Listener(connection, "Kantilever"))
            {
                listener.SubscribeCommands<bestellingGoedgekeurd>(container);
                listener.SubscribeCommands<factuurAangemaakt>(container);
                listener.SubscribeCommands<betaaldeFactuurAfgemeld>(container);
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
