using System;
using System.Security.Cryptography.X509Certificates;
using Crossover.AirTicket.Core.Common;

namespace Crossover.AirTicket.Core.Cqrs
{
    public interface IEvent
    {
    }

    public class Event :Entity,IEvent
    {
        public DateTime TimeStamp { get; set; }
        public string Name { get; set; }
        
        public Event()
        {
            TimeStamp = DateTime.Now;
            Name = GetType().Name;
        }
     
    }
}