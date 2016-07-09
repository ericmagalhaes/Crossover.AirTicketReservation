using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Messaging;
using MongoDB.Messaging.Service;
using MongoDB.Messaging.Subscription;

namespace ConsoleApplication1
{
    class Program
    {
        private static string EmailQueueName = "EmailNotificationQueue";
        private static string MongoDbConnection = "MongoServerSettings";
        private static MessageService _messageService = null;

        static void Main(string[] args)
        {
           

            // on service or application start
            
            MessageQueue.Default.Configure(c => c
                .Connection(MongoDbConnection)
                .Queue(s => s
                    .Name(EmailQueueName)
                    .Priority(MessagePriority.High)
                    .ResponseQueue("ReplyQueueName")
                    .Retry(5)
                )
                );

            var message = MessageQueue.Default.Publish(m => m.Queue("SleepQueue").Data(new SleepMessage())).Result;
            
            _messageService = new MessageService();
            _messageService.Start();
            Console.ReadLine();
        }

        public class SleepMessage
        {
        }

        public class SleepHandler : IMessageSubscriber
        {
            public MessageResult Process(ProcessContext context)
            {
                // get message data
                var sleepMessage = context.Data<SleepMessage>();

                // Do processing here

                return MessageResult.Successful;
            }

            public void Dispose()
            {
                // free resources
            }
        }
    }
}
