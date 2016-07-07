using System;
using System.Linq;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Core.Exception;
using Crossover.AirTicket.Core.Repository;
using Crossover.AirTicket.Logic.Domain;
using Crossover.AirTicket.Logic.Query.Flights;
using MongoDB.Driver;
using MongoDB.Driver.Linq;


namespace Crossover.AirTicket.Logic.Handlers
{
    public class FlightsQueryHandler:
        IQueryHandler<FlightSearchPublicUserQuery,FlightSearchPublicUserQueryResult>,
        IQueryHandler<FlightBookingConfirmQuery,FlightBookingConfirmQueryResult>,
        IQueryHandler<FlightBookingRequestQuery, FlightBookingRequestQueryResult>
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

        public FlightBookingRequestQueryResult Retrieve(FlightBookingRequestQuery query)
        {
            var selectedFligh = _flightRepository.AsQueryable().FirstOrDefault(flight => flight.Id == query.FlightId);

            if (selectedFligh == null)
                throw new AirTicketBusinessException("Flight not found");

            var flightBookingRequestQueryResult = new FlightBookingRequestQueryResult();
            flightBookingRequestQueryResult.Departure = selectedFligh.Departure;
            flightBookingRequestQueryResult.From = selectedFligh.From.Name;
            flightBookingRequestQueryResult.To = selectedFligh.To.Name;
            flightBookingRequestQueryResult.Price = selectedFligh.Price;
            flightBookingRequestQueryResult.Seats = selectedFligh.OpenSeats().Length;
            flightBookingRequestQueryResult.FlightId = selectedFligh.Id;

            return flightBookingRequestQueryResult;
        }
    }
}
