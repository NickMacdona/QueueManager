using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job;

namespace QueueManager
{
    public interface IQueue
    {
        bool AddJob(BaseJob job);
        BaseJob? DequeueFirst();

        BaseJob? DequeuePriority(int priority, String type);
    }
}
