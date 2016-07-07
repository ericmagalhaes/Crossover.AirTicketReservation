using System;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Logic.Domain;

namespace Crossover.AirTicket.Logic.Query.Flights
{
    public class FlightSearchPublicUserQuery : IQuery
    {
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Depart { get; set; }
        public DateTime Return { get; set; }
    }
}
