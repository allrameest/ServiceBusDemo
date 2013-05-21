using System;
using Rhino.ServiceBus.Sagas;

namespace Messages
{
    public class ArticlesReserved : ISagaMessage
    {
        public Guid CorrelationId { get; set; }
    }
}