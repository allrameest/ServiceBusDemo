using System;
using Rhino.ServiceBus.Hosting;

namespace Backend
{
    internal class Program
    {
        private static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                                                          HandleException((Exception)args.ExceptionObject);

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