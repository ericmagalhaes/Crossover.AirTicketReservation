using System;
using System.Collections.Generic;
using Crossover.AirTicket.Core.Common;
using Crossover.AirTicket.Logic.Commands.Flights;
using Crossover.AirTicket.Logic.Events;

namespace Crossover.AirTicket.Logic.Domain
{
    public class BookingRequest:AggregateRoot
    {
        public BookingRequest()
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
           public static BookingRequest Create(RequestBookingCommand command)
           {
                var request = new FlightBookingCreatedEvent(command.FlightId,command.Passengers.Length,command.Passengers,command.UserId);
                var flightBooking = new BookingRequest();
                flightBooking.RaiseEvent(request);
                return flightBooking;
            }
        }

        public static class Adapter
        {
            public static Booking Booking(BookingRequest bookingRequest)
            {
                var booking = new Booking(bookingRequest.FlightId, bookingRequest.ReservedSeats, bookingRequest.User);
                booking.RequestId = bookingRequest.Id;
                return booking;
            }
        }

    }
}