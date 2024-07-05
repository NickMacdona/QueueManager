using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueManager
{
    public interface IQueue
    {
        void AddJob(Job job);
        Job? Dequeue();
        string QueueName { get; }
        int Type { get; }
    }
}
