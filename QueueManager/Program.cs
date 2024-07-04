﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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
            jobManager.StartProcessing();
        }
    }

    public class JobManager
    {
        private int _jobCount;
        private List<Job> _jobs;
        private Dictionary<int, Queue<Job>> _queues;
        private readonly ILogger _logger;

        public JobManager(int jobCount, ILogger logger)
        {
            _jobCount = jobCount;
            _jobs = new List<Job>();
            _queues = new Dictionary<int, Queue<Job>>()
            {
                { 1, new Queue<Job>() },
                { 2, new Queue<Job>() },
                { 3, new Queue<Job>() },
                { 4, new Queue<Job>() }
            };
            _logger = logger;
        }

        public void CreateJobs()
        {
            JobCreator jobCreator = new JobCreator(_jobCount);
            _jobs = jobCreator.GenerateJobs();

            foreach (var job in _jobs)
            {
                _queues[job.QueueType].Enqueue(job);
                _logger.LogInformation($"Created {job.Name} in Queue {job.QueueType} with priority {job.Priority} and runtime {job.RunTime}ms");
            }
        }

        public void StartProcessing()
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
                while (_queues[queueType].Count > 0)
                {
                    Job job = _queues[queueType].Dequeue();
                    string logMessage = $"Processing {job.Name} from Queue {queueType} with priority {job.Priority} for {job.RunTime}ms";

                    writer.WriteLine(logMessage);

                    Console.WriteLine(logMessage);
                    _logger.LogInformation(logMessage);

                    Thread.Sleep(job.RunTime);
                }
            }
        }
    }
}