using System;
using System.Collections.Generic;

namespace Crossover.AirTicket.Core.Common
{
    public abstract class AggregateRoot
    {
        public string Id { get; set; }
        
        protected void RaiseEvent(DomainEvent domainEvent)
        {
            (this as dynamic).Apply((dynamic) domainEvent);
        }

    }
}