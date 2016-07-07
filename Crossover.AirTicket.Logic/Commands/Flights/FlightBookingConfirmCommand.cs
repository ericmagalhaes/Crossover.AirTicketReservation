using System;
using Crossover.AirTicket.Core.Cqrs;

namespace Crossover.AirTicket.Logic.Commands.Flights
{
    public class FlightBookingConfirmCommand : ICommand
    {
        public Guid FlightId { get; set; }
        public Guid BookingId { get; set; }

    }
}