using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rabbitmq_demo;
using RabbitMQ.Client;
using Autofac;
using System.Threading;

namespace AlgmeenBerichtenTesten
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "cursistm07",
                UserName = "team1",
                Password = "team1"
            };

            using (var sender = new Sender(factory, "Prufail"))
            {

                //Maak builder
                var builder = new ContainerBuilder();

                builder.RegisterReceiverFor<MonteurService, NieuweAfspraakGemaakt>();
                builder.RegisterReceiverFor<MonteurService, Steekproef>();

                builder.RegisterInstance(sender).As<ISender>();
                builder.RegisterInstance(factory).As<IConnectionFactory>();



                using (var container = builder.Build())
                using (var listener = new rabbitmq_demo.Listener(factory, "Prufail"))
                {
                    //
                    listener.SubscribeEvents<NieuweAfspraakGemaakt>(container);
                    listener.SubscribeEvents<Steekproef>(container);

                    //Actions meegeven
                    listener.Received += Show_new_message;

                    //Programma laten lopen
                    Console.WriteLine("Listening!");




                    while (true)
                    {
                        var key = Console.ReadLine();
                        if (key == "Close")
                        {
                            Console.WriteLine("PersonCreated Listener will now close!");
                            Thread.Sleep(1000);
                            break;
                        }
                        else if (key == "Send A")
                        {
                            Console.WriteLine("Versturen Afmelden");
                            try
                            {
                                sender.PublishCommand(new Afmelden() { AfspraakId = 2, IsAfgemeld = true });
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                        }
                        else if (key == "Send W")
                        {
                            Console.WriteLine("Versturen WerkzaamhedenGewijzigd bericht!");

                            try
                            {
                                sender.PublishCommand(new WerkzaamhedenWijzigen() { AfspraakId = 3, Werkzaamheden = new List<Werkzaamheid>() });
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                    }

                }


            }

            
        }

        private void Listen_to(Listener listener)
        {
            Console.WriteLine("Luister naar bericht: ");
            var userdefinedtype = Console.ReadLine();

            Type modeltype = Type.GetType(userdefinedtype);
            listener.SubscribeEvents<>(container);

        }

        private static void Show_new_message(object sender, ReceivedEventArgs e)
        {
            Console.WriteLine(e);
        }
    }
}
