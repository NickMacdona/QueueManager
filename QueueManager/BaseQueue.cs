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
    public class BaseQueue : IQueue
    {

        private static int _jobID = 0;
        private readonly ConcurrentDictionary<int, BaseJob> _queue = new ConcurrentDictionary<int, BaseJob>();

        public BaseQueue()
        {
        }

        public bool AddJob(BaseJob job)
        {
            int newId = Interlocked.Increment(ref _jobID);
            job.Name = $"{job.Name}_{newId}";
            return _queue.TryAdd(newId, job);
            //returns false if we fail to add the job
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
            Console.WriteLine("Method is not available for non-priorty indexed queue");
            return null;
        }

        public BaseJob? DequeuePriority()
        {
            Console.WriteLine("Method is not available for non-priorty indexed queue");
            return null;
        }

    }
}
