using System;
using Rhino.ServiceBus.Sagas;

namespace Messages
{
    public class CancelUnfinishedOrder : ISagaMessage
    {
        public Guid CorrelationId { get; set; }
    }
}