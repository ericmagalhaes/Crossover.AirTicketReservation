using System;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Core.Queue;
using Crossover.AirTicket.Core.Repository;
using Crossover.AirTicket.Logic.Events;
using Crossover.AirTicket.Logic.Queue;
using MongoDB.Bson;

namespace Crossover.AirTicket.Logic.Handlers
{
    public class BookingEventHandler :
        IEventHandler<BookingCreatedEvent>,
        IEventHandler<BookingRejectedEvent>
    {
        private readonly IEmailNotificationQueue<EmailNotification> _emailNotification = null;
        private readonly IRepository<EmailNotification> _emailNotificationRepository = null;
        public BookingEventHandler(IEmailNotificationQueue<EmailNotification> mailNotificationQueue, IRepository<EmailNotification> emailNotificationRepository)
        {
            _emailNotification = mailNotificationQueue;
            _emailNotificationRepository = emailNotificationRepository;
        }

        public void Raise(BookingCreatedEvent evt)
        {
            var emailNotification = new EmailNotification();
            var emailQueue = new EmailQueue();

            emailNotification.Email = evt.Email;
            emailNotification.Content = evt.ToJson();

            _emailNotificationRepository.Save(emailNotification);

            emailQueue.EmailNotifyId = emailNotification.Id;
            _emailNotification.Enqueue(emailNotification);
        }

        public void Raise(BookingRejectedEvent evt)
        {
            var emailNotification = new EmailNotification();

            _emailNotificationRepository.Save(emailNotification);
            _emailNotification.Enqueue(emailNotification);
        }
    }
}