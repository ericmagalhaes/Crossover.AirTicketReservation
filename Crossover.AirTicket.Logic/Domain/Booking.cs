using System;
using System.Collections.Generic;
using Crossover.AirTicket.Core.Common;
using Crossover.AirTicket.Core.Exception;

namespace Crossover.AirTicket.Logic.Domain
{
    public class Booking:Entity
    {
        
        public string FlightId { get; private set; }
        public string User { get; private set; }
        public int ReservedSeats { get; private set; }
        public bool Checkout { get; private set; }
        public bool Canceled { get; private set; }
        public bool Closed { get; private set; }
        public IList<Passenger> Passengers { get; private set; }


        public Booking(string flightId, int seats,string user)
        {
            if (flightId == null)
                throw new AirTicketBusinessException("flight cannot be empty");
            if (user == null)
                throw new AirTicketBusinessException("user cannot be empty");
            if (seats <= 0)
                throw new AirTicketBusinessException("at least one seat must be selected");
            
            FlightId = flightId;
            User = user;
            ReservedSeats = seats;
            Closed = false;
            Passengers = new List<Passenger>();
        }

        public Booking AddPassenger(Passenger passenger)
        {
            Passengers.Add(passenger);
            return this;
        }

        public Booking Close()
        {
            Closed = true;
            return this;
        }
        
    }
}