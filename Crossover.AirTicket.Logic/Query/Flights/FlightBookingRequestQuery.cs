using System;
using Crossover.AirTicket.Core.Cqrs;

namespace Crossover.AirTicket.Logic.Query.Flights
{
    public class FlightBookingRequestQuery : IQuery
    {
        public Guid FlightId { get; set; }
    }
}
