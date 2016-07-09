namespace Crossover.AirTicket.Logic.Repositories
{
    public class CommandResponse
    {
        public static CommandResponse Ok = new CommandResponse(true);
        public static CommandResponse Fail = new CommandResponse(false);
        public CommandResponse(bool success = false,string entityId="")
        {
            Success = success;
            EntityId = entityId;
        }

        public string RequestId { get; set; }
        public bool Success { get; private set; }
        public string EntityId { get; private set; }
        public string Description { get; set; }
        
        
    }
}