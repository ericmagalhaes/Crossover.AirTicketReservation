using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crossover.AirTicket.Core.Exception;
using Crossover.AirTicket.Core.Repository;
using Crossover.AirTicket.Logic.Domain;

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

        public CommandResponse CreateBookingRequest(FlightBooking flightBooking)
        {
            lock (_reservationLock)
            {
                var flight = _flightRepository.AsQueryable().FirstOrDefault(f => f.Id == flightBooking.FlightId);

                if (flight == null)
                    throw new AirTicketBusinessException("flight not found");

                var booking = new Booking(flightBooking.FlightId, flightBooking.ReservedSeats, flightBooking.User);

                CommandResponse commandResponse = null;

                try
                {
                    flight = flight.ReserveSeats(booking);
                    _flightRepository.Save(flight);
                    booking = _bookingRepository.Save(booking);
                    
                    commandResponse = new CommandResponse(booking.Id, true);
                    return commandResponse;
                }
                catch (AirTicketBusinessException)
                {
                    commandResponse = new CommandResponse(null);
                    return commandResponse;
                }
            }
        }
    }
}
