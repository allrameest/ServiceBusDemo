using System;
using Rhino.ServiceBus.Sagas;

namespace Messages
{
    public class PaymentCompleted : ISagaMessage
    {
        public string Reference { get; set; }

        public Guid CorrelationId { get; set; }
    }
}