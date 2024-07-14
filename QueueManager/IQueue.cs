using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job;

namespace Queue
{
    public interface IQueue
    {
        bool AddJob(BaseJob job);
        BaseJob? DequeueFirst();

        BaseJob? DequeuePriority(int queuetype);

        BaseJob? DequeuePriority();
    }
}
