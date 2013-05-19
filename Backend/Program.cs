using System;
using Rhino.ServiceBus.Hosting;

namespace Backend
{
    internal class Program
    {
        private static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                                                          HandleException((Exception) args.ExceptionObject);

            //var builder = new ContainerBuilder();
            //var container = builder.Build();
            //new RhinoServiceBusConfiguration()
            //    .UseAutofac(container)
            //    .Configure();

            new DefaultHost()
                .Start<BootStrapper>();
            
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