using System.Collections.Generic;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Logic.Domain;

namespace Crossover.AirTicket.Logic.Query.Flights
{
    public class FlightSearchPublicUserQueryResult : IQueryResult
    {
        public IEnumerable<OpenFlights> Flights { get; set; } 
    }
}
