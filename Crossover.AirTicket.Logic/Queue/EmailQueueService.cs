using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crossover.AirTicket.Core.Queue;
using Crossover.AirTicket.Logic.Events;
using MongoDB.Messaging;
using MongoDB.Messaging.Service;

namespace Crossover.AirTicket.Logic.Queue
{
    public class MongoEmailNotificationQueue : IEmailNotificationQueue<EmailNotification>
    {

        private static string EmailQueueName = "EmailNotificationQueue";
        private static string MongoDbConnection = "MongoServerSettingsQueue";
        private static MessageService _messageService = null;
        public MongoEmailNotificationQueue()
        {

        }

        public static void Init()
        {

            MessageQueue.Default.Configure(c => c
    .Connection(MongoDbConnection)
    .Queue(s => s
        .Name(EmailQueueName)
        .Retry(5)));

            MessageQueue.Default.Configure(c =>
            c.Connection(MongoDbConnection)
            .Subscribe(s =>
            s.Queue(EmailQueueName)
            .Handler<EmailQueueHandler>()
            .Workers(4)));
            _messageService = new MessageService();
            _messageService.Start();
           

        }

        public void Enqueue(EmailNotification emailNotification)
        {
            var message = MessageQueue.Default.Publish(
                m => m.Queue(EmailQueueName)
                    .Data(emailNotification)).Result;
        }

    }
}
