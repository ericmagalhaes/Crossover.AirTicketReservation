using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Messaging;
using MongoDB.Messaging.Service;

namespace ConsoleApplication1
{
    class Program
    {
        private static string EmailQueueName = "EmailNotificationQueue";
        private static string MongoDbConnection = "MongoServerSettings";
        private static MessageService _messageService = null;
        static void Main(string[] args)
        { 

        MessageQueue.Default.Configure(c => c
    .Connection(MongoDbConnection)
    .Queue(s => s
        .Name(EmailQueueName)
        .Retry(5)));

            //MessageQueue.Default.Configure(c =>
            //c.Connection(MongoDbConnection)
            //.Subscribe(s =>
            //s.Queue(EmailQueueName)
            //.Handler<EmailQueueHandler>()
            //.Workers(4)));
            _messageService = new MessageService();
            _messageService.Start();
            var message = MessageQueue.Default.Publish(
               m => m.Queue(EmailQueueName)
                   .Data(new object()));
        }
    }
}
