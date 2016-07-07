using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crossover.AirTicket.Api.Controllers;
using NUnit.Framework;


namespace Crossover.AirTicket.Acceptance.Tests
{
    [TestFixture]
    public class PublicUserTests
    {
        [Test]
        public void PublicUserShouldSearchMultiplesFlightsCriterias()
        {
            var flightController = new FlightsController();
            flightController.Flight()
        }
        [Test]
        public void PublicUserShouldReserveAirTicket()
        {
            
        }
        [Test]
        public void PublicUsersShouldCancelReservationFrom48HrsAndUpTo4HrsFromDeparture()
        {
        }
        
    }
    [TestFixture]
    public class AirlineStaffTests
    {
        [Test]
        public void AirLineStaffShouldCancelABooking()
        {
            
        }

        public void AirLineStaffShouldCancelWholeFlight()
        {

        }
    }
}
