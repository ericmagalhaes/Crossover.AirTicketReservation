using System;
using System.Linq;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Core.Repository;
using Crossover.AirTicket.Logic.Domain;
using Crossover.AirTicket.Logic.Query.Flights;
using MongoDB.Driver;
using MongoDB.Driver.Linq;


namespace Crossover.AirTicket.Logic.Handlers
{
    public class FlightsQueryHandler:
        IQueryHandler<FlightSearchPublicUserQuery,FlightSearchPublicUserQueryResult>,
        IQueryHandler<FlightBookingConfirmQuery,FlightBookingConfirmQueryResult>
    {
        private readonly IRepository<Flight> _flightRepository = null;
        public FlightsQueryHandler(IRepository<Flight> flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public FlightSearchPublicUserQueryResult Retrieve(FlightSearchPublicUserQuery query)
        {
            var flightSearchPublicUserQueryResult = new FlightSearchPublicUserQueryResult();
            var flights =
                _flightRepository.AsQueryable()
                    .Where(f => f.From.Id == query.From && f.To.Id == query.To);

            flightSearchPublicUserQueryResult.Flights = flights.Select(flight => new OpenFlights()
            {
                Departure = flight.Departure,
                From = flight.From.Name,
                To = flight.To.Name,
                FlightId = flight.Id,
                OpenSeats = flight.Seats.Length,

            });
            return flightSearchPublicUserQueryResult;

        }

        public FlightBookingConfirmQueryResult Retrieve(FlightBookingConfirmQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
