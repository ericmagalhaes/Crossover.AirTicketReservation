using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crossover.AirTicket.Domain;

namespace Crossover.AirTicket.Api.Controllers
{
    [Route("Flights")]
    public class FlightsController : ApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public FlightsController(IQuer)
        {
            _flightServices = flightServices;
        }

        /// <summary>
        /// Resposible to filter flight based on search criteria
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="departure"></param>
        /// <param name="returning"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Flight> Get(string from, string to, string departure, string returning)
        {
            return _flightServices.Flights(from, to, departure, returning);
        }
    }
}
