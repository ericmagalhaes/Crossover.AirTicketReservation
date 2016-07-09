using Crossover.AirTicket.Core.Common;

namespace Crossover.AirTicket.Logic.Domain
{
    public class EmailNotification : Entity
    {
        public string To { get; set; }
        public string Content { get; set; }
        public bool Success { get; set; }
        public int Retry { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
    }
}
