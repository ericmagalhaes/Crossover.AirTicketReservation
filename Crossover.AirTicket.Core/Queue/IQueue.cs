using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossover.AirTicket.Core.Queue
{
    public interface IEmailNotificationQueue<TEntity> where TEntity:class
    {
        void Enqueue(TEntity entity);
    }
}
