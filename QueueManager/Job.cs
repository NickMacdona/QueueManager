using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueManager
{
    public class Job
    {
        public string Name { get; set; }
        public int QueueType { get; set; }
        public int Priority { get; set; }
        public int RunTime { get; set; }
    }
}
