using System;
using Crossover.AirTicket.Core.Cqrs;

namespace Crossover.AirTicket.Logic.Query.Flights
{
    public class FlightBookingConfirmQuery : IQuery
    {
        public Guid FlightId { get; set; }
        public Guid BookingId { get; set; }
    }
}