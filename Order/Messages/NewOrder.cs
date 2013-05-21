using System;
using System.Collections.Generic;
using Rhino.ServiceBus.Sagas;

namespace Messages
{
    public class NewOrder : ISagaMessage
    {
        public string OrderNumber { get; set; }
        public string Customer { get; set; }
        public List<string> Articles { get; set; }
        public decimal Amount { get; set; }

        public Guid CorrelationId { get; set; }
    }
}