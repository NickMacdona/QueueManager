using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueManager
{
    public class JobCreator
    {
        private int _jobCount;

        public JobCreator(int jobCount)
        {
            _jobCount = jobCount;
        }

        public List<Job> GenerateJobs()
        {
            List<Job> jobs = new List<Job>();
            Random random = new Random();
            for (int i = 0; i < _jobCount; i++)
            {
                Job job = new Job
                {
                    Name = $"Job_{i + 1}",
                    QueueType = random.Next(1, 5),
                    Priority = random.Next(1, 5),
                    RunTime = random.Next(50, 5001)
                };
                jobs.Add(job);
            }
            return jobs;
        }
    }
}
