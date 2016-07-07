using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Crossover.AirTicket.Domain
{
    public class Flight : AggregateRoot
    {
        public String Name { get; set; }
        public Seat[] Seats { get; private set; }
        public DateTime Departure { get; set; }
        public DateTime Landing { get; set; }
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

    public class FlightMap
    {
    }

    public class FlightRow
    {
        
    }

    public interface IFlightServices
    {
        IEnumerable<Flight> Flights(string from, string to, object departure, string returning);
    }

    public class FlightServices : IFlightServices
    {
    }



    public class PublicUser 
    {

    }

    public class AirlineStaff 
    {
    }

    public class BookingService
    {
        public Booking Book(Flight flight, PublicUser user, int seats)
        {
            throw new NotImplementedException();
        }

        public void Cancel(string bookId)
        {
            throw new NotImplementedException();
        }

        public void Checkout(string bookId)
        {

        }

        public void Print(string bookId)
        {
        }

        public void SetSeats(string bookId, string[] seats)
        {

        }

    }

    public interface IPayment
    {
        void Refund(string paymentId);
    }

    public class AirTicketBusinessException : AirTicketException
    {
        public AirTicketBusinessException(string message)
        {

        }
    }

    [TestFixture]
    public class PublicUserContextTest
    {
        [Test]
        public void PublicUserShouldLoginThirdParty()
        {
        }

        [Test]
        public void PublicUserShouldSearchMultiplesFlights()
        {

        }

        [Test]
        public void PublicUsersShouldMakeOnlinePayment()
        {

        }
        [Test]
        public void PublicUsersShouldPerformOnlineCheckout()
        {

        }
        [Test]
        public void PublicUserShouldCancelOnline()
        {

        }

        [Test]
        public void PublicUsersShouldReceiverAlertBefore48HrsFromDeparture()
        {

        }
    }
}