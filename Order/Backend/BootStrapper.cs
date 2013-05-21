using Autofac;
using Rhino.ServiceBus.Autofac;
using Rhino.ServiceBus.Internal;
using Rhino.ServiceBus.Sagas.Persisters;

namespace Backend
{
    public class BootStrapper : AutofacBootStrapper
    {
        public BootStrapper()
            : base(BuildContainer())
        {
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(InMemorySagaPersister<>)).As(typeof(ISagaPersister<>)).SingleInstance();
            return builder.Build();
        }
    }
}