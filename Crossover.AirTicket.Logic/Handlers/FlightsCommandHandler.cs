using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Logic.Commands.Flights;

namespace Crossover.AirTicket.Logic.Handlers
{
    public class FlightsCommandHandler:
        ICommandHandler<FlightBookingCommand>,
        ICommandHandler<FlightBookingConfirmCommand>,
        ICommandHandler<FlightBookingPaymentCommand>
    {
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
