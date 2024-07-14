using Job;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Queue
{
    //consider use of a miniheap in the future to scale better into larger queues
    //todo implement an index dictionary
    //todo implement logging
    public class PriorityQueue : IQueue
    {

        private static int _jobID = 0;
        private readonly ConcurrentDictionary<int, BaseJob> _queue = new ConcurrentDictionary<int, BaseJob>();
        private readonly ConcurrentDictionary<int, int> _priorityIndex = new ConcurrentDictionary<int, int>();

        public PriorityQueue()
        {
        }

        public bool AddJob(BaseJob job)
        {
            int newId = Interlocked.Increment(ref _jobID);
            job.Name = $"{job.Name}_{newId}";
            _priorityIndex.TryAdd(newId, job.Priority);
            return _queue.TryAdd(newId, job);
            //returns false if we fail to add the job
        }

        private int FindNextJobToRemove()
        {
            int[,] nextjob = new int[1, 2];
            foreach(var kvp in _priorityIndex)
            {
                if(kvp.Value > nextjob[0,1] && kvp.Key < nextjob[0,0])
                {
                    nextjob[0,1] = kvp.Value;
                    nextjob[0,0] = kvp.Key;
                }
            }
            return nextjob[0,0];
        }

        public BaseJob? DequeueFirst()
        {
            try
            {
                int minKey = _queue.Keys.Min(); // Throws InvalidOperationException if dictionary is empty
                if (_queue.TryRemove(minKey, out BaseJob removedJob))// We handle nulls in this already so we probably don't care if this is a null reference
                {
                    Console.WriteLine($"Successfully removed the item with key {minKey}");
                    return removedJob;
                } 
                else
                {
                    Console.WriteLine($"Failed to remove the item with key {minKey}. The key may not exist.");
                    return null;
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
            
        }

        public BaseJob? DequeuePriority(int queuetype)
        {
            try
            {
                int minKey = FindNextJobToRemove(); // returns first key by default
                if (_queue.TryRemove(minKey, out BaseJob removedJob))// We handle nulls in this already so we probably don't care if this is a null reference
                {
                    Console.WriteLine($"Successfully removed the item with key {minKey}");
                    return removedJob;
                }
                else
                {
                    Console.WriteLine($"Failed to remove the item with key {minKey}. The key may not exist.");
                    return null;
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public BaseJob? DequeuePriority()
        {
            Console.WriteLine("Method is not available for non-priorty indexed queue");
            return null;
        }

    }
}
