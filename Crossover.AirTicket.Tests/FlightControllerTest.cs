using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crossover.AirTicket.Core.Cqrs;
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


        [Test]
        public void FlightSearchPublicUserQueryTest()
        {
            var flightController = new FlightsController(_queryDispatcher,_commandDispatcher);
            var flightSearchPublicUserQuery = new FlightSearchPublicUserQuery();
            //flightSearchPublicUserQuery.Depart = DateTime.Now.AddHours(15);
            //flightSearchPublicUserQuery.Return = DateTime.Now.AddDays(5).AddHours(14);
            flightSearchPublicUserQuery.From = Bootstrap.Orlando;
            flightSearchPublicUserQuery.To = Bootstrap.Paris;
            var flightSearchPublicUserQueryResult = flightController.Flights(flightSearchPublicUserQuery);

            Assert.AreEqual(flightSearchPublicUserQueryResult.Flights.Count(), 5);
            
        }
    }
}
