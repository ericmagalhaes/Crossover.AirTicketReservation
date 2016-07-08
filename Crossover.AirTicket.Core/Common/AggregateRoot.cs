using System;
using System.Collections.Generic;

namespace Crossover.AirTicket.Core.Common
{
    public abstract class AggregateRoot : Entity
    {
        private readonly Queue<DomainEvent> _uncommittedEvents = new Queue<DomainEvent>();
        

        protected void RaiseEvent(DomainEvent @event)
        {
            _uncommittedEvents.Enqueue(@event);
            (this as dynamic).Apply((dynamic)@event);
        }

    }
    public class DomainEvent
    {
        public DateTime TimeStamp { get; private set; }

        public DomainEvent()
        {
            TimeStamp = DateTime.Now;
        }
    }
}