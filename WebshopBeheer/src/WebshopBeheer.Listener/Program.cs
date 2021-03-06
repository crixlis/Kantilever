﻿using Autofac;
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

            var connection = new ConnectionFactory()
                .FromEnvironment();

            var builder = new ContainerBuilder();
            builder.RegisterReceiverFor<WebshopBeheerService, BestellingAangemaakt>();
            builder.RegisterReceiverFor<WebshopBeheerService, BestellingGoedgekeurd>();
            builder.RegisterReceiverFor<WebshopBeheerService, FactuurAangemaakt>();
            builder.RegisterReceiverFor<WebshopBeheerService, BetaaldeFactuurAfgemeld>();
            builder.Register(s => new Sender(connection, "Kantilever")).As<ISender>();

            using (var container = builder.Build())
            using (var listener = new rabbitmq_demo.Listener(connection, "Kantilever"))
            {
                listener.SubscribeEvents<BestellingAangemaakt>(container);
                listener.SubscribeEvents<BestellingGoedgekeurd>(container);
                listener.SubscribeEvents<FactuurAangemaakt>(container);
                listener.SubscribeEvents<BetaaldeFactuurAfgemeld>(container);
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
