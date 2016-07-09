using System;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Core.Queue;
using Crossover.AirTicket.Core.Repository;
using Crossover.AirTicket.Logic.Domain;
using Crossover.AirTicket.Logic.Events;
using Crossover.AirTicket.Logic.Queue;
using MongoDB.Bson;

namespace Crossover.AirTicket.Logic.Handlers
{
    public class BookingEventHandler : 
        IEventHandler<BookingCreatedEvent>,
        IEventHandler<BookingRequestCreateEvent>
    {
        public void Raise(BookingCreatedEvent command)
        {
            
        }

        public void Raise(BookingRequestCreateEvent command)
        {
            
        }
    }
    public class NotificationEventHandler :
        IEventHandler<BookingCreatedEvent>,
        IEventHandler<BookingRequestedRejectEvent>
    {
        private readonly IEmailNotificationQueue<EmailNotification> _emailNotification = null;
        private readonly IRepository<EmailNotification> _emailNotificationRepository = null;
        private readonly IRepository<Event> _eventRepository = null;
        public NotificationEventHandler(IEmailNotificationQueue<EmailNotification> mailNotificationQueue, 
            IRepository<EmailNotification> emailNotificationRepository,
            IRepository<Event> eventRepository )
        {
            _emailNotification = mailNotificationQueue;
            _emailNotificationRepository = emailNotificationRepository;
            _eventRepository = eventRepository;
        }

        public void Raise(BookingCreatedEvent evt)
        {
            var emailNotification = new EmailNotification();
            var emailQueue = new EmailQueue();

            emailNotification.To = evt.Email;
            emailNotification.Content = evt.ToJson();

            _emailNotificationRepository.Save(emailNotification);

            emailQueue.EmailNotifyId = emailNotification.Id;
            _emailNotification.Enqueue(emailNotification);
        }

        public void Raise(BookingRequestedRejectEvent evt)
        {
            var emailNotification = new EmailNotification();
            var emailQueue = new EmailQueue();

         

            _emailNotificationRepository.Save(emailNotification);

            emailQueue.EmailNotifyId = emailNotification.Id;
            _emailNotification.Enqueue(emailNotification);
        }
    }

    
}