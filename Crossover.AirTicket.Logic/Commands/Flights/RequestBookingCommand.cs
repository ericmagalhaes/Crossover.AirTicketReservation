using System;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Logic.Domain;

namespace Crossover.AirTicket.Logic.Commands.Flights
{
    public class RequestBookingCommand : ICommand
    {
        public string FlightId { get; set; }
        public Passenger[] Passengers { get; set; }
        public string UserId { get; set; }

    }
}