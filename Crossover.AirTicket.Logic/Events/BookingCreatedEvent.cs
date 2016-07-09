using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Logic.Domain;

namespace Crossover.AirTicket.Logic.Events
{
    public class BookingCreatedEvent :Event
    {
        public  Booking BookingInfo { get; set; }
        

        public BookingCreatedEvent(string requestId, Booking bookingInfo)
        {
            RequestId = requestId;
            BookingInfo = bookingInfo;
        }

        public string Email { get; set; }
    }
}