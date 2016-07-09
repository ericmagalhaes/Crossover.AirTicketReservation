using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crossover.AirTicket.Core.Common;
using Crossover.AirTicket.Core.Exception;
using Crossover.AirTicket.Core.Repository;
using Crossover.AirTicket.Logic.Domain;
using MongoDB.Bson;

namespace Crossover.AirTicket.Logic.Repositories
{
    public class FlightBookingRepository
    {
        private readonly IRepository<Flight> _flightRepository = null;
        private readonly IRepository<Booking> _bookingRepository = null;
        
        private readonly object _reservationLock = new object();
        public FlightBookingRepository(IRepository<Flight> flightRepository, IRepository<Booking> bookingRepository)
        {
            _flightRepository = flightRepository;
            _bookingRepository = bookingRepository;
        
        }

        public CommandResponse CreateBookingRequest(BookingRequest bookingRequest)
        {
            lock (_reservationLock)
            {
                CommandResponse commandResponse = null;
                try
                {
                    var flight = _flightRepository.AsQueryable().FirstOrDefault(f => f.Id == bookingRequest.FlightId);
                    if (flight == null)
                        throw new AirTicketBusinessException(bookingRequest.Id, "flight not found");
                    var booking = BookingRequest.Adapter.Booking(bookingRequest);
                    flight = flight.ReserveSeats(booking);
                    _flightRepository.Save(flight);
                    booking = _bookingRepository.Save(booking);
                    commandResponse = new CommandResponse(true, booking.Id);
                    return commandResponse;
                }
                catch (AirTicketBusinessException businessException)
                {
                    commandResponse = CommandResponse.Fail;
                    commandResponse.Description = businessException.Message;
                    return commandResponse;
                }
            }
        }
    }

    internal class EventSourcing
    {
    }
}
