﻿using Autofac;
using Microsoft.EntityFrameworkCore;
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

            var connection = new ConnectionFactory()
                .FromEnvironment();

            var builder = new ContainerBuilder();
            builder.RegisterReceiverFor<WebshopListenerService, BetaaldeFactuurAfgemeld>();
            builder.RegisterReceiverFor<WebshopListenerService, ArtikelAanCatalogusToegevoegd>();
            builder.Register(s => new Sender(connection, "Kantilever")).As<ISender>();

            var options = new DbContextOptionsBuilder<WebshopContext>()
               .UseSqlServer(@"Server=.\SQLEXPRESS;Database=ArtikelenKantilever;Trusted_Connection=true")
               .Options;

            builder.RegisterType<WebshopContext>().As<IWebshopContext>();
            builder.RegisterInstance(options);

            using (var container = builder.Build())
            using (var listener = new rabbitmq_demo.Listener(connection, "Kantilever"))
            {
                listener.SubscribeEvents<BetaaldeFactuurAfgemeld>(container);
                listener.SubscribeEvents<ArtikelAanCatalogusToegevoegd>(container);
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
