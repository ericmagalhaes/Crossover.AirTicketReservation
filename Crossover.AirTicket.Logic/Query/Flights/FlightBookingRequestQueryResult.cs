using System;
using System.Collections.Generic;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Logic.Domain;

namespace Crossover.AirTicket.Logic.Query.Flights
{
    public class FlightBookingRequestQueryResult : IQueryResult
    {
        public Location From { get; set; }
        public Location To { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Return { get; set; }
        public IEnumerable<Seat> Seats { get; set; } 
    }
}
