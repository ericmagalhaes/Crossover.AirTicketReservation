namespace Crossover.AirTicket.Logic.Repositories
{
    public class CommandResponse
    {
        public CommandResponse(string requestId, bool success = false)
        {
            Success = success;
            RequestId = requestId;
        }

        public bool Success { get; private set; }
        public string RequestId { get; private set; }
        public string Description { get; set; }
    }
}