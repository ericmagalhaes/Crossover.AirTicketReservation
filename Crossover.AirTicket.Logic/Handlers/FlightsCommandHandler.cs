using System;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Core.Repository;
using Crossover.AirTicket.Logic.Commands.Flights;
using Crossover.AirTicket.Logic.Domain;

namespace Crossover.AirTicket.Logic.Handlers
{
    public class FlightsCommandHandler:
        ICommandHandler<FlightBookingCommand>,
        ICommandHandler<FlightBookingConfirmCommand>,
        ICommandHandler<FlightBookingPaymentCommand>
    {
        private readonly IRepository<Flight> _flightRepository = null;
        public FlightsCommandHandler(IRepository<Flight> flightRepository)
        {
            _flightRepository = flightRepository;
        }
        public void Execute(FlightBookingCommand command)
        {
            throw new NotImplementedException();
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
}
