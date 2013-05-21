using System.Collections.Generic;

namespace Messages
{
    public class SendConfirmationEmail
    {
        public string OrderNumber { get; set; }
        public string Customer { get; set; }
        public List<string> Articles { get; set; }
    }
}