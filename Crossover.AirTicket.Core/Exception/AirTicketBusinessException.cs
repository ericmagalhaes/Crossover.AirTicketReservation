namespace Crossover.AirTicket.Core.Exception
{
    public class AirTicketBusinessException : AirTicketException
    {
        public string BusinessCode { get; set; }
        public string RequestId { get; set; }
        public AirTicketBusinessException( string message) : base(message)
        {

        }
        public AirTicketBusinessException(string requestId,string message):base(message)
        {

        }
        public AirTicketBusinessException(string requestId,string businessCode, string message):base(message)
        {
            BusinessCode = businessCode;
        }
    }
}