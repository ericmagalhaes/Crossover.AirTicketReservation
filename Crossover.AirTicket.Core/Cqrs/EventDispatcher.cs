using System;
using SimpleInjector;

namespace Crossover.AirTicket.Core.Cqrs
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly Container _container;

        public EventDispatcher(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("kernel");
            }

            _container = container;
        }

        public void Raise<TParameter>(TParameter command) where TParameter : IEvent
        {
            var handler = _container.GetInstance<IEventHandler<TParameter>>();
            
                handler.Raise(command);
            
            
        }

    }
}