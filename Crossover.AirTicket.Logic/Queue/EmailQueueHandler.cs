using System;
using System.Linq;
using Crossover.AirTicket.Core.DI;
using Crossover.AirTicket.Core.Infraestructure;
using Crossover.AirTicket.Core.Repository;
using Crossover.AirTicket.Logic.Domain;
using MongoDB.Messaging;
using MongoDB.Messaging.Subscription;

namespace Crossover.AirTicket.Logic.Queue
{
    public class EmailQueueHandler : IMessageSubscriber
    {
        private readonly IRepository<EmailNotification> _emailRepository = null;
        private readonly IEmailService _emailService = null;
        private readonly IEmailMessage _mailMessage = null;

        public EmailQueueHandler()
        {
            _emailRepository = AirTicketDI.Get<IRepository<EmailNotification>>();
            _emailService = AirTicketDI.Get<IEmailService>();
            _mailMessage = AirTicketDI.Get<IEmailMessage>();

        }
        public void Dispose()
        {
            
        }

        public MessageResult Process(ProcessContext processContext)
        {
            var emailQueue = processContext.Data<EmailQueue>();
            var emailContent = _emailRepository.AsQueryable().FirstOrDefault(e => e.Id == emailQueue.EmailNotifyId);
            if (emailContent == null)
            {
                processContext.Message.Description = "Email message not found. Message ID:" + emailQueue.EmailNotifyId;
                return MessageResult.Warning;
            }
            _mailMessage.Content = emailContent.Content;
            _mailMessage.To = emailContent.To;
            _mailMessage.From = emailContent.From;
            _mailMessage.Subject = emailContent.Subject;
                        
            if (_emailService.Send(_mailMessage))
            {
                emailContent.Success = true;
                _emailRepository.Save(emailContent);
                return MessageResult.Successful;
            }
            if (emailContent.Retry == 5)
            {
                processContext.Message.Description = "Retries exceeded(5)";
                return MessageResult.Warning;
            }
            emailContent.Retry++;
            _emailRepository.Save(emailContent);
            return MessageResult.None;
        }
    }
}