using Autofac;
using RabbitMQ.Client;
using rabbitmq_demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BestelService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "BestelService";
            Console.WriteLine("BestelService wacht op een inkomend bericht...");

            var connection = new ConnectionFactory { HostName = "cursistm07", UserName = "manuel", Password = "manuel" };

            var builder = new ContainerBuilder();
            builder.RegisterReceiverFor<BestelService, bestellingAanmaken>();

            using (var container = builder.Build())
            using (var listener = new Listener(connection, "Kantilever"))
            {
                listener.SubscribeCommands<bestellingAanmaken>(container);
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
