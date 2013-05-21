using System;
using System.Threading;
using Messages;
using Rhino.ServiceBus;

namespace Backend
{
    public class ReserveArticlesConsumer : ConsumerOf<ReserveArticles>
    {
        private readonly IServiceBus _bus;

        public ReserveArticlesConsumer(IServiceBus bus)
        {
            _bus = bus;
        }

        public void Consume(ReserveArticles message)
        {
            Console.WriteLine("Reserving...");
            Thread.Sleep(5000);
            Console.WriteLine("Done reserving...");
            
            _bus.Send(new ArticlesReserved
                {
                    CorrelationId = message.CorrelationId
                });
        }
    }
}