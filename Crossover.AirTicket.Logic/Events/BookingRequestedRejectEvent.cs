using System;
using Crossover.AirTicket.Core.Cqrs;

namespace Crossover.AirTicket.Logic.Events
{
    public class BookingRequestedRejectEvent : Event
    {
        
        public string Description { get; private set; }
        public BookingRequestedRejectEvent(string requestId, string description)
        {
            RequestId = requestId;
            Description = description;
        }
    }
}