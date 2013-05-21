using Backend;
using FakeItEasy;
using Messages;
using NUnit.Framework;
using Rhino.ServiceBus;
using SharpTestsEx;

namespace Tests
{
    [TestFixture]
    public class OrderSagaTests
    {
        [Test]
        public void PaymentCompleted_WhenArticlesNotReserved_DeliverArticles()
        {
            var bus = A.Fake<IServiceBus>();
            var orderSaga = new OrderSaga(bus);
            orderSaga.State.ArticlesReserved = false;

            orderSaga.Consume(new PaymentCompleted());

            orderSaga.IsCompleted.Should().Be.False();
            orderSaga.State.PaymentCompleted.Should().Be.True();
            A.CallTo(() => bus.Send(A<object>.That.IsInstanceOf(typeof(DeliverArticles)))).MustNotHaveHappened();
            A.CallTo(() => bus.Send(A<object>.That.IsInstanceOf(typeof(SendConfirmationEmail)))).MustNotHaveHappened();
        }

        [Test]
        public void PaymentCompleted_WhenArticlesReserved_DeliverArticles()
        {
            var bus = A.Fake<IServiceBus>();
            var orderSaga = new OrderSaga(bus);
            orderSaga.State.ArticlesReserved = true;

            orderSaga.Consume(new PaymentCompleted());

            orderSaga.IsCompleted.Should().Be.True();
            orderSaga.State.PaymentCompleted.Should().Be.True();
            A.CallTo(() => bus.Send(A<object>.That.IsInstanceOf(typeof(DeliverArticles)))).MustHaveHappened();
            A.CallTo(() => bus.Send(A<object>.That.IsInstanceOf(typeof(SendConfirmationEmail)))).MustHaveHappened();
        }
    }
}