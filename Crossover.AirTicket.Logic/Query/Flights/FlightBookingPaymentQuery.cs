using System;
using Crossover.AirTicket.Core.Cqrs;

namespace Crossover.AirTicket.Logic.Query.Flights
{
    public class FlightBookingPaymentQuery : IQuery
    {
        public string FlightId { get; set; }
        public string BookingId { get; set; }
        public double TotalPrice { get; set; }
    }
}