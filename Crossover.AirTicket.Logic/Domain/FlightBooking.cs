using System;
using System.Collections.Generic;
using Crossover.AirTicket.Core.Common;
using Crossover.AirTicket.Logic.Events;

namespace Crossover.AirTicket.Logic.Domain
{
    public class FlightBooking:AggregateRoot
    {
        public FlightBooking()
        {
            Id = Guid.NewGuid().ToString("B");
        }
        public string FlightId { get; private set; }
        public string User { get; private set; }
        public int ReservedSeats { get; private set; }
        public bool Checkout { get; private set; }
        public bool Canceled { get; private set; }
        public bool Closed { get; private set; }
        public IList<Passenger> Passengers { get; private set; }
        

        public void Apply(FlightBookingCreatedEvent evt)
        {
            FlightId = evt.FlightId;
            User = evt.UserId;
            ReservedSeats = evt.Seats;
            Passengers = evt.Passengers;
            Checkout = false;
            Canceled = false;
            Closed = false;
            
        }

        public Booking BookingInfo()
        {
            var booking = new Booking(FlightId,ReservedSeats,User);
            foreach (var passenger in Passengers)
            {
                booking.AddPassenger(passenger);
            }
            booking.Close();
            return booking;
        }

        public static class Factory
        {
            public static FlightBooking Create(string flightId,int seats,Passenger[] passengers,string userId)
            {
                var request = new FlightBookingCreatedEvent(flightId,seats,passengers,userId);
                var flightBooking = new FlightBooking();
                flightBooking.RaiseEvent(request);
                return flightBooking;
            }
        }

    }
}