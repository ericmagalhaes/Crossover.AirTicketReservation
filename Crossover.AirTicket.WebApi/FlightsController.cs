using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Logic.Commands.Flights;
using Crossover.AirTicket.Logic.Query.Flights;

namespace Crossover.AirTicket.WebApi
{
    [Route("Flights")]
    public class FlightsController : ApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public FlightsController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [Route(""),HttpGet]
        public FlightSearchPublicUserQueryResult Flights(FlightSearchPublicUserQuery query)
        {
            return _queryDispatcher.Dispatch<FlightSearchPublicUserQuery, FlightSearchPublicUserQueryResult>(query);
        }

        [Route("{FlightId}/Booking"), HttpGet]
        public FlightBookingRequestQueryResult FlightBooking([FromUri]FlightBookingRequestQuery query)
        {
            return _queryDispatcher.Dispatch<FlightBookingRequestQuery, FlightBookingRequestQueryResult>(query);
        }

        [Route("{FlightId}/Booking"), HttpPost]
        public IHttpActionResult FlightBooking([FromBody] RequestBookingCommand command)
        {
            _commandDispatcher.Dispatch(command);
            return Ok();
        }
        [Route("{FlightId}/Bookings/"), HttpGet]
        public FlightBookingsQueryResult FlightBookings([FromUri] FlightBookingsQuery query)
        {
            return _queryDispatcher.Dispatch<FlightBookingsQuery, FlightBookingsQueryResult>(query);
            
        }
        [Route("Flight/{FlightId}/Booking/{BookingId}"), HttpGet]
        public FlightBookingConfirmQueryResult FlightBooking([FromUri] FlightBookingConfirmQuery query)
        {
            return _queryDispatcher.Dispatch<FlightBookingConfirmQuery, FlightBookingConfirmQueryResult>(query);

        }

        [Route("Flight/{FlightId}/Booking/{BookingId}/Payment"), HttpGet]
        public FlightBookingPaymentQueryResult FlightBooking([FromUri] FlightBookingPaymentQuery query)
        {
            return _queryDispatcher.Dispatch<FlightBookingPaymentQuery, FlightBookingPaymentQueryResult>(query);

        }


    }
}
