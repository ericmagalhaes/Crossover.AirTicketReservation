using System;
using System.Collections.Generic;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Logic.Domain;

namespace Crossover.AirTicket.Logic.Query.Flights
{
    public class FlightBookingRequestQueryResult : IQueryResult
    {
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Departure { get; set; }
        public int Seats { get; set; } 
        public double Price { get; set; }
        public string FlightId { get; set; }
    }
}
