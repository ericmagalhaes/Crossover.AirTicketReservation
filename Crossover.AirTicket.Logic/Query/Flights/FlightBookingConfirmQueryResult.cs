using System;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Logic.Domain;

namespace Crossover.AirTicket.Logic.Query.Flights
{
    public class FlightBookingConfirmQueryResult : IQueryResult
    {
        public Guid FlightId { get; set; }
        public Guid BookingId { get; set; }
        public Passenger[] Passengers { get; set; }
        public double TotalPrice { get; set; }
    }
}