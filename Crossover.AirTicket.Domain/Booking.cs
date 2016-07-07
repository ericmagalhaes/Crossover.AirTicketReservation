using System;

namespace Crossover.AirTicket.Domain
{
    public class Booking
    {
        public Guid Id { get; private set; }
        public Flight Flight { get; private set; }
        public PublicUser User { get; private set; }
        public IPayment Payment { get; private set; }
        public Seat[] SelectedSeats { get; private set; }
        public bool Checkout { get; private set; }
        public bool Canceled { get; private set; }


        public Booking(Flight flight, PublicUser user, int seats, IPayment payment)
        {
            if (flight == null)
                throw new AirTicketBusinessException("flight cannot be empty");
            if (user == null)
                throw new AirTicketBusinessException("user cannot be empty");
            if (seats <= 0)
                throw new AirTicketBusinessException("at least one seat must be selected");
            if (payment == null)
                throw new AirTicketBusinessException("no payment method was selected");

            Id = Guid.NewGuid();
            Flight = flight;
            User = user;
            Payment = payment;
            SelectedSeats = new Seat[seats];
        }

        public void ConfirmPayment()
        {
            Checkout = true;
        }

        public void Cancel()
        {
            Canceled = true;
            if (Checkout)
                Payment.Refund(Id.ToString());
            Flight.UndoSeats(SelectedSeats);

        }
    }
}