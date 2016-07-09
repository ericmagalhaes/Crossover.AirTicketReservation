using Crossover.AirTicket.Core.Common;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Logic.Domain;

namespace Crossover.AirTicket.Logic.Events
{
    public class FlightBookingCreatedEvent : DomainEvent
    {
        public  string FlightId { get; set; }
        public Passenger[] Passengers { get; set; }
        public  int Seats { get; set; }
        public string UserId { get; set; }

        public FlightBookingCreatedEvent(string flightId, int seats, Passenger[] passengers, string userId)
        {
            FlightId = flightId;
            Seats = seats;
            Passengers = passengers;
            UserId = userId;
        }
    }
}