using System;
using System.Linq;
using Crossover.AirTicket.Core.Common;
using Crossover.AirTicket.Core.Exception;
using MongoDB.Bson.Serialization.Attributes;

namespace Crossover.AirTicket.Logic.Domain
{
    public class Flight : Entity
    {
        public String Name { get; set; }
        public Location To { get; set; }
        public Location From { get; set; }
        public Seat[] Seats { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Landing { get; set; }
        public bool Closed { get; set; }
        public double Price { get; set; }
        
        [BsonIgnore]
        private readonly object _reservationLock = new object();
        /// <summary>
        /// Responsible to reserve seats on the plane if no seats are available or the 
        /// requested numbers of seats are unavailable a business exception is throw
        /// </summary>
        /// <param name="booking">Booking</param>
        /// <param name="selectedSeats">Selected Seats</param>
        /// <returns>Flight</returns>
        public Flight ReserveSeats(Booking booking, Seat[] selectedSeats)
        {
            lock (_reservationLock)
            {
                var availableSeats = Seats.Count(s => !s.Reserved);
                var requestedSeats = selectedSeats.Length;
                if (availableSeats == 0)
                    throw new AirTicketBusinessException("No seats available");
                if (requestedSeats > availableSeats)
                {
                    var diffSeats = requestedSeats - availableSeats;
                    throw new AirTicketBusinessException($"Only {diffSeats} are available");
                }
                Array.ForEach(selectedSeats, (seat) =>
                {
                    var flightSeat = Seats.FirstOrDefault(s => s.Id == seat.Id);
                    if (flightSeat == null)
                        throw new AirTicketBusinessException("Seat not found");
                    flightSeat.Reserve(booking);
                });
                return this;
            }
        }

        public void UndoSeats(Seat[] selectedSeats)
        {

        }
    }
}