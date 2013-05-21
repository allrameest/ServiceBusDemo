using System;
using System.Collections.Generic;
using Messages;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Hosting;

namespace Shop
{
    internal class Program
    {
        private static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                                                          HandleException((Exception) args.ExceptionObject);

            var host = new DefaultHost();
            host.Start<BootStrapper>();

            var bus = (IServiceBus) host.Bus;


            Console.WriteLine("Press enter to place order.");
            Console.ReadLine();

            Guid correlationId = Guid.NewGuid();
            bus.Send(new NewOrder
                {
                    OrderNumber = "1",
                    Customer = "Erik",
                    Articles = new List<string> {"1001", "1002", "1003"},
                    Amount = 1337,
                    CorrelationId = correlationId
                });

            Console.WriteLine("Pay $1337? y/n");
            var input = Console.ReadLine() ?? "";

            if (input.Equals("y"))
            {
                bus.Send(new PaymentCompleted
                    {
                        Reference = "1234567890",
                        CorrelationId = correlationId
                    });

                Console.WriteLine("Order receipt...");
            }
            else
            {
                bus.Send(new PaymentFailed
                    {
                        CorrelationId = correlationId
                    });
            }

            Console.ReadLine();
        }

        private static void HandleException(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex);
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}