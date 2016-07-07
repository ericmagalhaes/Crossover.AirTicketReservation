using System;
using Crossover.AirTicket.Core.Cqrs;

namespace Crossover.AirTicket.Logic.Query.Flights
{
    public class FlightBookingsQueryResult : IQueryResult
    {
        public Guid[] BookingId { get; set; }
    }
}