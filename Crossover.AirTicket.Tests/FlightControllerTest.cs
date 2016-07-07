using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Logic.Commands.Flights;
using Crossover.AirTicket.Logic.Demo;
using Crossover.AirTicket.Logic.Domain;
using Crossover.AirTicket.Logic.Query.Flights;
using Crossover.AirTicket.WebApi;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SimpleInjector;

namespace Crossover.AirTicket.Tests
{
    [TestFixture]
    public class FlightControllerTest
    {
        private IQueryDispatcher _queryDispatcher = null;
        private ICommandDispatcher _commandDispatcher = null;

        [SetUp]
        public void Init()
        {

            var container = Bootstrap.InitContainer();
            _queryDispatcher = new QueryDispatcher(container);
            _commandDispatcher = new CommandDispatcher(container);
            //Bootstrap.InitDatabase();
        }

        private void InitDatabase()
        {
        }


        /// <summary>
        /// please first init the database and count the results before assert the method.
        /// </summary>
        [Test]
        public void FlightSearchPublicUserQueryTest()
        {
            var flightController = new FlightsController(_queryDispatcher,_commandDispatcher);
            var flightSearchPublicUserQuery = new FlightSearchPublicUserQuery();
            flightSearchPublicUserQuery.From = Bootstrap.Orlando;
            flightSearchPublicUserQuery.To = Bootstrap.Paris;
            var flightSearchPublicUserQueryResult = flightController.Flights(flightSearchPublicUserQuery);

            Assert.AreEqual(flightSearchPublicUserQueryResult.Flights.Count(), 37);
            
        }
        [Test]
        public void FlightBookingRequestQueryTest()
        {
            var flightController = new FlightsController(_queryDispatcher, _commandDispatcher);
            var flightBookingRequestQuery = new FlightBookingRequestQuery();
            flightBookingRequestQuery.FlightId = "577eb8bbf3c09b2a78fd6f47";
            var flightBookingRequestQueryResult = flightController.FlightBooking(flightBookingRequestQuery);
            Assert.AreEqual(flightBookingRequestQueryResult.FlightId, flightBookingRequestQuery.FlightId);
        }

        [Test]
        public void FlightBookingCommandTest()
        {
            var flightController = new FlightsController(_queryDispatcher, _commandDispatcher);
            var flightBookingCommand = new FlightBookingCommand();
            flightBookingCommand.FlightId = "577eb8bbf3c09b2a78fd6f47";
            flightBookingCommand.Passengers = new Passenger[]
            {
                new Passenger()
                {
                    Name = "Eric Magalhães",
                    DocumentId = "3.293.691",
                    Email = "ericmagalhaes@gmail.com"
                },
                new Passenger()
                {
                    Name = "Alex Weiler Magalhães",
                    DocumentId = "7.890.763",
                    Email = "ericmagal@hotmail.com"
                }
            };
            flightController.FlightBooking(flightBookingCommand);
            Assert.AreEqual(true,true);

        }
    }
}
