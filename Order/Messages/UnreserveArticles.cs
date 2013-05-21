using System.Collections.Generic;

namespace Messages
{
    public class UnreserveArticles
    {
        public string OrderNumber { get; set; }
        public List<string> Articles { get; set; }
    }
}