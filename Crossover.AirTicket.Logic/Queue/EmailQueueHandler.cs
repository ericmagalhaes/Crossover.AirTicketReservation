using System;
using MongoDB.Messaging;
using MongoDB.Messaging.Subscription;

namespace Crossover.AirTicket.Logic.Queue
{
    public class EmailQueueHandler : IMessageSubscriber
    {
        public void Dispose()
        {
            
        }

        public MessageResult Process(ProcessContext processContext)
        {
            return MessageResult.Successful;
            
        }
    }
}