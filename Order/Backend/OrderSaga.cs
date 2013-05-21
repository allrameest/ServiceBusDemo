using System;
using System.Collections.Generic;
using Messages;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Sagas;

namespace Backend
{
    public class OrderSaga :
        ISaga<OrderState>,
        InitiatedBy<NewOrder>,
        Orchestrates<PaymentCompleted>,
        Orchestrates<PaymentFailed>,
        Orchestrates<ArticlesReserved>,
        Orchestrates<CancelUnfinishedOrder>
    {
        private readonly IServiceBus _bus;

        public OrderSaga(IServiceBus bus)
        {
            _bus = bus;
            State = new OrderState();
        }

        public Guid Id { get; set; }
        public bool IsCompleted { get; set; }
        public OrderState State { get; set; }

        public void Consume(NewOrder message)
        {
            State.OrderNumber = message.OrderNumber;
            State.Customer = message.Customer;
            State.Articles = message.Articles;
            State.Amount = message.Amount;

            Console.WriteLine("Order received. Reserving articles...");
            _bus.Send(new ReserveArticles
                {
                    OrderNumber = State.OrderNumber,
                    Articles = State.Articles,
                    CorrelationId = Id
                });

            _bus.DelaySend(DateTime.Now.AddSeconds(10), new CancelUnfinishedOrder
                {
                    CorrelationId = Id
                });
        }

        public void Consume(PaymentCompleted message)
        {
            Console.WriteLine("Payment completed.");
            State.PaymentCompleted = true;
            DeliverIfDone();
        }

        public void Consume(PaymentFailed message)
        {
            Console.WriteLine("Payment failed.");
            CancelOrder();
        }

        public void Consume(ArticlesReserved message)
        {
            Console.WriteLine("Articles reserved.");
            State.ArticlesReserved = true;
            DeliverIfDone();
        }

        public void Consume(CancelUnfinishedOrder message)
        {
            Console.WriteLine("Cancelling unfinished order.");
            CancelOrder();
        }

        private void DeliverIfDone()
        {
            if (State.PaymentCompleted && State.ArticlesReserved)
            {
                Console.WriteLine("Deliver articles.");
                _bus.Send(new DeliverArticles
                    {
                        OrderNumber = State.OrderNumber,
                        Customer = State.Customer,
                        Articles = State.Articles
                    });
                _bus.Send(new SendConfirmationEmail
                    {
                        OrderNumber = State.OrderNumber,
                        Customer = State.Customer,
                        Articles = State.Articles
                    });
                IsCompleted = true;
            }
        }

        private void CancelOrder()
        {
            Console.WriteLine("Unreserving articles.");
            _bus.Send(new UnreserveArticles
                {
                    OrderNumber = State.OrderNumber,
                    Articles = State.Articles
                });
            IsCompleted = true;
        }
    }

    public class OrderState
    {
        public string OrderNumber { get; set; }
        public string Customer { get; set; }
        public List<string> Articles { get; set; }
        public decimal Amount { get; set; }

        public bool PaymentCompleted { get; set; }
        public bool ArticlesReserved { get; set; }
    }
}