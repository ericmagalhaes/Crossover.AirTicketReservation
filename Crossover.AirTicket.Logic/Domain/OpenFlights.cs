using System;

namespace Crossover.AirTicket.Logic.Domain
{
    public class OpenFlights
    {
        public string FlightId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Departure { get; set; }
        public int OpenSeats { get; set; }
        public double Price { get; set; }
    }
}