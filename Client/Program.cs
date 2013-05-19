using System;
using Messages;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Hosting;

namespace Client
{
    internal class Program
    {
        private const int NumberOfMessages = 100;

        private static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                                                          HandleException((Exception) args.ExceptionObject);

            var host = new DefaultHost();
            host.Start<BootStrapper>();

            var bus = (IServiceBus) host.Bus;

            while (true)
            {
                Console.WriteLine("Write a message.");
                var message = Console.ReadLine();

                for (int i = 0; i < NumberOfMessages; i++)
                {
                    bus.Send(new HelloWorldMessage
                        {
                            Content = message
                        });
                }

                Console.WriteLine("Message '{0}' sent {1} times.", message, NumberOfMessages);
            }
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