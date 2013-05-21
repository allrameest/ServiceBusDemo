using Messages;
using Rhino.ServiceBus;

namespace Backend
{
    public class DeliverArticlesConsumer : ConsumerOf<DeliverArticles>
    {
        public void Consume(DeliverArticles message)
        {
        }
    }
}