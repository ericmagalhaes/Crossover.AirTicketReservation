using System;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Crossover.AirTicket.Core.Infraestructure
{
    public class SendGridMailService : IEmailService
    {
        
        public bool Send(IEmailMessage mailMessage)
        {
            String apiKey = AirTicketSettings.SendGridApiKey;
            var sendGridClient = new SendGrid.SendGridAPIClient(apiKey, "https://api.sendgrid.com");

            Email from = new Email(mailMessage.From);
            String subject = mailMessage.Subject;
            Email to = new Email(mailMessage.To);
            Content content = new Content("text/plain", "Textual content");
            Mail mail = new Mail(from, subject, to, content);
            String ret = mail.Get();
            string requestBody = ret;
            dynamic response = sendGridClient.client.mail.send.post(requestBody: requestBody);
            return response.StatusCode == 200;
        }
    }
}