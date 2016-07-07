using System;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Logic.Domain;

namespace Crossover.AirTicket.Logic.Commands.Flights
{
    public class FlightBookingCommand : ICommand
    {
        public Guid FlightId { get; set; }
        public Passenger[] Passengers { get; set; }

    }
}