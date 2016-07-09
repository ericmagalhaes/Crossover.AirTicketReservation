using System;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Core.Security;
using Crossover.AirTicket.Logic.Commands.Flights;
using Crossover.AirTicket.Logic.Domain;
using Crossover.AirTicket.Logic.Events;
using Crossover.AirTicket.Logic.Repositories;

namespace Crossover.AirTicket.Logic.Handlers
{
    public class BookingCommandHandler :
        ICommandHandler<RequestBookingCommand>,
        ICommandHandler<FlightBookingConfirmCommand>,
        ICommandHandler<FlightBookingPaymentCommand>
    {
        #region Attributes

        private readonly FlightBookingRepository _bookingRepository = null;
        private readonly ISecurityContext _securityContext = null;
        private readonly IEventDispatcher _eventDispatcher = null;

        #endregion

        #region Constructors

        public BookingCommandHandler(FlightBookingRepository bookingRepository, ISecurityContext securityContext,IEventDispatcher eventDispatcher)
        {
            _bookingRepository = bookingRepository;
            _securityContext = securityContext;
            _eventDispatcher = eventDispatcher;
        }

        #endregion

        public void Execute(RequestBookingCommand command)
        {
            var bookingRequest = BookingRequest.Factory.Create(command);
            var request = new BookingRequestCreateEvent(bookingRequest.Id, bookingRequest);
            _eventDispatcher.Raise(request);
            var response = _bookingRepository.CreateBookingRequest(bookingRequest);
            if (!response.Success)
            {
                var rejected = new BookingRequestedRejectEvent(bookingRequest.Id, response.Description);
                _eventDispatcher.Raise(rejected);
                return;
            }
            var bookingInfo = bookingRequest.BookingInfo();
            var bookingCreated = new BookingCreatedEvent(bookingRequest.Id, bookingInfo);
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

    public class BookingRequestCreateEvent : Event
    {
        public BookingRequest BookingRequest { get; private set; }
        public BookingRequestCreateEvent(string requestId,BookingRequest bookingRequest)
        {
            BookingRequest = bookingRequest;
            RequestId = requestId;
        }
    }
}
