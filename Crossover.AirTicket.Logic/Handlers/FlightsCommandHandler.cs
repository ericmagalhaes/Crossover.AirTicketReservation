using System;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Core.Security;
using Crossover.AirTicket.Logic.Commands.Flights;
using Crossover.AirTicket.Logic.Domain;
using Crossover.AirTicket.Logic.Events;
using Crossover.AirTicket.Logic.Repositories;

namespace Crossover.AirTicket.Logic.Handlers
{
    public class FlightsCommandHandler :
        ICommandHandler<FlightBookingCommand>,
        ICommandHandler<FlightBookingConfirmCommand>,
        ICommandHandler<FlightBookingPaymentCommand>
    {
        #region Attributes

        private readonly FlightBookingRepository _flightBookingRepository = null;
        private readonly ISecurityContext _securityContext = null;
        private readonly IEventDispatcher _eventDispatcher = null;

        #endregion

        #region Constructors

        public FlightsCommandHandler(FlightBookingRepository flightBookingRepository, ISecurityContext securityContext,IEventDispatcher eventDispatcher)
        {
            _flightBookingRepository = flightBookingRepository;
            _securityContext = securityContext;
            _eventDispatcher = eventDispatcher;
        }

        #endregion



        public void Execute(FlightBookingCommand command)
        {
            var flightBooking = FlightBooking.Factory.Create(
                command.FlightId,
                command.Passengers.Length,
                command.Passengers,
                _securityContext.UserId);
            var response = _flightBookingRepository.CreateBookingRequest(flightBooking);
            if (!response.Success)
            {
                var bookingRejected = new BookingRejectedEvent(flightBooking.Id,response.Description);
                bookingRejected.Email = _securityContext.UserEmail;
                _eventDispatcher.Raise(bookingRejected);
                return;
            }
            var bookingInfo = flightBooking.BookingInfo();
            var bookingCreated = new BookingCreatedEvent(response.RequestId, bookingInfo);
            bookingCreated.Email = _securityContext.UserEmail;
            _eventDispatcher.Raise(bookingCreated);
        }

        public void Execute(FlightBookingConfirmCommand command)
        {
            throw new NotImplementedException();
        }

        public void Execute(FlightBookingPaymentCommand command)
        {
            throw new NotImplementedException();
        }
    }

    public class BookingRejectedEvent : IEvent
    {
        public string Email { get; set; }
        public BookingRejectedEvent(string id, string description)
        {
            throw new NotImplementedException();
        }
    }
}
