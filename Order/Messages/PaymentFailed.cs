using System;
using Rhino.ServiceBus.Sagas;

namespace Messages
{
    public class PaymentFailed : ISagaMessage
    {
        public Guid CorrelationId { get; set; }
    }
}