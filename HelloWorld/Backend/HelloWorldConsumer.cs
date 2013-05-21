using System;
using System.Threading;
using Messages;
using Rhino.ServiceBus;

namespace Backend
{
    public class HelloWorldConsumer : ConsumerOf<HelloWorldMessage>
    {
        private static int _messagesConsumed;

        public void Consume(HelloWorldMessage message)
        {
            //throw new Exception();
            Thread.Sleep(30);
            Console.WriteLine("{0} {1}", _messagesConsumed, message.Content);
            _messagesConsumed++;
        }
    }
}