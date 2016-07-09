using System.Collections.Generic;

namespace Crossover.AirTicket.Core.Infraestructure
{
    public interface IEmailMessage
    {
        string To { get; set; }
        string Subject { get; set; }
        string From { get; set; }
        string Content { get; set; }
        Dictionary<string,string> Headers { get; set; }

    }
}