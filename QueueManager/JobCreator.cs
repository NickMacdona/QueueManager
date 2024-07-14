using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job;
using Queue;

namespace JobFactory

{
    public class JobCreator : IJobCreator
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
                    Name = $"Job",
                    QueueType = random.Next(1, 5),
                    Priority = random.Next(1, 5),
                    RunTime = random.Next(50, 1000)
                };
                jobs.Add(job);
            }
            Console.Write($"Adding {_jobCount} jobs to the queue");
            _jobCount = 0;
            return jobs;
        }

        public void AddNewJobsToQueue(Queue.IQueue queue)
        {
            List<BaseJob> _listOfJobs = GenerateJobs();

            for (int i = 0; i < _listOfJobs.Count; i++)
            {
                queue.AddJob(_listOfJobs[i]);
            }

        }

        public void AddSingleJob(IQueue queue, String name, int queuetype, int priority, int runtime)
        {
            BaseJob job = new BaseJob
            {
                Name = name,
                QueueType = queuetype,
                Priority = priority,
                RunTime = runtime
            };
            queue.AddJob(job);
            Console.Write($"Adding single job {name} to the queue");

        }
    }
}
