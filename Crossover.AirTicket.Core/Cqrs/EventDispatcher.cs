using System;
using Crossover.AirTicket.Core.Repository;
using SimpleInjector;

namespace Crossover.AirTicket.Core.Cqrs
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly Container _container;
        private readonly IRepository<Event> _eventRepository = null;
        public EventDispatcher(IRepository<Event> eventRepository,Container container)
        {
            _eventRepository = eventRepository;
            _container = container;
        }

        public void Raise<TParameter>(TParameter command) where TParameter : Event
        {
            var handlers = _container.GetAllInstances<IEventHandler<TParameter>>();
            if (_eventRepository != null)
                _eventRepository.Save(command);
            foreach (var eventHandler in handlers)
            {
                eventHandler.Raise(command);
            }
            
        }

    }
}