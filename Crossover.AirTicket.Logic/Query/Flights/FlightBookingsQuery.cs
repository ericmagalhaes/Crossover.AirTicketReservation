using System;
using Crossover.AirTicket.Core.Cqrs;

namespace Crossover.AirTicket.Logic.Query.Flights
{
    public class FlightBookingsQuery : IQuery
    {
        public string FlightId { get; set; }
    }
}