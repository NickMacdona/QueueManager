using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Job;
using Queue;
using JobFactory;
using System.Linq.Expressions;

namespace QueueManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LoggerFactory.CreateLogger<Program>();

            int jobCount = 10;
            JobManager jobManager = new JobManager(jobCount, logger);
            jobManager.CreateJobs();
            jobManager.PrintQueue();
            jobManager.CreateJobs();
            jobManager.PrintQueue();
        }
    }

    public class JobManager
    {
        private int _jobCount;
        private List<BaseJob> _jobs;
        private readonly Queue.BaseQueue _queue;
        private readonly ILogger _logger;

        public void PrintQueue()
        {
            while (true)
            {
                try
                {
                    BaseJob? job = _queue.DequeueFirst();
                    string logMessage = $"Processing {job.Name} from Queue {job.QueueType} with priority {job.Priority} for {job.RunTime} ms";
                    Console.WriteLine(logMessage);
                    Thread.Sleep(job.RunTime);
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Queue is empty or null job found");
                    break; 
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Queue is empty or null job found");
                    break;
                }
                

            }
        }

        public JobManager(int jobCount, ILogger logger)
        {
            _jobCount = jobCount;
            _jobs = new List<BaseJob>();
            _queue = new Queue.BaseQueue();
            _logger = logger;
        }

        public void CreateJobs()
        {
            JobCreator jobCreator = new JobCreator(_jobCount);
            jobCreator.AddNewJobsToQueue(_queue);
        }

        public void StartProcessing() //need to fix this to new queue implementation
        {
            List<Task> tasks = new List<Task>();

            for (int i = 1; i <= 4; i++)
            {
                int queueType = i;
                tasks.Add(Task.Run(() => ProcessQueue(queueType)));
            }

            Task.WaitAll(tasks.ToArray());
        }

        private void ProcessQueue(int queueType)
        {
            string logFileName = $"Queue_{queueType}_Log.txt";
            using (StreamWriter writer = new StreamWriter(logFileName))
            {
                while (_queue.DequeueFirst != null)
                {
                    BaseJob? job = _queue.DequeueFirst();
                    string logMessage = $"Processing {job.Name} from Queue {queueType} with priority {job.Priority} for {job.RunTime}ms";

                    writer.WriteLine(logMessage);

                    Console.WriteLine(logMessage);
                    _logger.LogInformation(logMessage);

                    Thread.Sleep(job.RunTime);
                }

                Console.Write("Queue is empty or null job found");
            }
        }
    }
}
