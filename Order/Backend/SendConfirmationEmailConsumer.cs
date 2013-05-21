using Messages;
using Rhino.ServiceBus;

namespace Backend
{
    public class SendConfirmationEmailConsumer : ConsumerOf<SendConfirmationEmail>
    {
        public void Consume(SendConfirmationEmail message)
        {
        }
    }
}