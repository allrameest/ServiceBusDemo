using System;
using System.Collections.Generic;

namespace Messages
{
    public class ReserveArticles
    {
        public string OrderNumber { get; set; }
        public List<string> Articles { get; set; }
        public Guid CorrelationId { get; set; }
    }
}