using System;
using Crossover.AirTicket.Core.Common;
using Crossover.AirTicket.Core.Exception;

namespace Crossover.AirTicket.Logic.Domain
{
    public class Seat : Entity
    {
        public String Number { get; private set; }
        public bool Reserved { get; private set; }
        public bool Closed { get; set; }

        public Booking Booking { get; private set; }
        public double Price { get; private set; }

        public Seat(string number)
        {
            Number = number;
        }

        public Seat SetPrice(double price)
        {
            Price = price;
            return this;
        }

        public Seat Reserve(Booking booking)
        {
            if (Reserved && Booking != booking)
                throw new AirTicketBusinessException("seat not available");
            Booking = booking;
            Reserved = true;
            return this;
        }

    }
}