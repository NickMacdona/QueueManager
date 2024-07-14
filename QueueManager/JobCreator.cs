using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job;
using Queue;

namespace JobFactory

{
    public class JobCreator
    {
        private int _jobCount;

        public JobCreator(int jobCount)
        {
            _jobCount = jobCount;
        }

        private List<BaseJob> GenerateJobs()
        {
            List<BaseJob> jobs = new List<BaseJob>();
            Random random = new Random();
            for (int i = 0; i < _jobCount; i++)
            {
                BaseJob job = new BaseJob
                {
                    Name = $"Job_{i + 1}",
                    QueueType = random.Next(1, 5),
                    Priority = random.Next(1, 5),
                    RunTime = random.Next(50, 1000)
                };
                jobs.Add(job);
            }
            _jobCount = 0;
            return jobs;
        }

        public void AddNewJobsToQueue(Queue.Queue queue)
        {
            List<BaseJob> _listOfJobs = GenerateJobs();

            for (int i = 0; i < _listOfJobs.Count; i++ )
            {
                queue.AddJob(_listOfJobs[i]);
            }

        }
    }
}
