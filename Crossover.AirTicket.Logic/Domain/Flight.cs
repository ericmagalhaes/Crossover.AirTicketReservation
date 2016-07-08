using System;
using System.Linq;
using Crossover.AirTicket.Core.Common;
using Crossover.AirTicket.Core.Exception;
using MongoDB.Bson.Serialization.Attributes;

namespace Crossover.AirTicket.Logic.Domain
{
    public class Flight : Entity
    {
        public String Name { get; private set; }
        public Location To { get; private set; }
        public Location From { get; private set; }
        public Seat[] Seats { get; private set; }
        public DateTime Departure { get; private set; }
        public DateTime Landing { get; private set; }
        public bool Closed { get; private set; }
        public double Price { get; private set; }
        public int OpenSeats { get; private set; }

        public Flight(string name, Location from, Location to, DateTime departure, DateTime landing, int openSeats)
        {
            Name = name;
            From = from;
            To = to;
            Departure = departure;
            Landing = landing;
            OpenSeats = openSeats;
        }

        [BsonIgnore]
        private readonly object _reservationLock = new object();
        /// <summary>
        /// Responsible to reserve seats on the plane if no seats are available or the 
        /// requested numbers of seats are unavailable a business exception is throw
        /// </summary>
        /// <param name="booking">Booking</param>
        /// <returns>Flight</returns>
        public Flight ReserveSeats(Booking booking)
        {
            var availableSeats = OpenSeats;
            var requestedSeats = booking.ReservedSeats;
            if (availableSeats == 0)
                throw new AirTicketBusinessException("No seats available");
            if (requestedSeats > availableSeats)
            {
                var diffSeats = requestedSeats - availableSeats;
                throw new AirTicketBusinessException($"Only {diffSeats} are available");
            }
            OpenSeats = OpenSeats - requestedSeats;
            return this;

        }

        public Flight AjustPrice(double price)
        {
            Price = price;
            return this;
        }


    }
}