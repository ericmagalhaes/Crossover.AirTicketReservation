using System;

namespace Crossover.AirTicket.Core.Common
{
    public class DomainEvent
    {
        public DateTime TimeStamp { get; private set; }

        public DomainEvent()
        {
            TimeStamp = DateTime.Now;
        }
    }
}