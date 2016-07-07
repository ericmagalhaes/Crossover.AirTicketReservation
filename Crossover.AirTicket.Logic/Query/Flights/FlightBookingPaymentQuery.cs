using System;
using Crossover.AirTicket.Core.Cqrs;

namespace Crossover.AirTicket.Logic.Query.Flights
{
    public class FlightBookingPaymentQuery : IQuery
    {
        public Guid FlightId { get; set; }
        public Guid BookingId { get; set; }
        public double TotalPrice { get; set; }
    }
}