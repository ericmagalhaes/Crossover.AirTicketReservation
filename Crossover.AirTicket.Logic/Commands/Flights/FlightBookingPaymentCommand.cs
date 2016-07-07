using System;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Logic.Domain;

namespace Crossover.AirTicket.Logic.Commands.Flights
{
    public class FlightBookingPaymentCommand : ICommand
    {
        public Guid FlightId { get; set; }
        public Guid BookingId { get; set; }
        public CreditCard CreditCard { get; set; }

    }
}