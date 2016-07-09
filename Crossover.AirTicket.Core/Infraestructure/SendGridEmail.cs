using System.Collections.Generic;

namespace Crossover.AirTicket.Core.Infraestructure
{
    public class SendGridEmail : IEmailMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public string Content { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public SendGridEmail()
        {
            
        }
    }
}