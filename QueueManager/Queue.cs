namespace QueueManager
{
    //consider use of a miniheap in the future to scale better into larger queues
    //todo implement an index dictionary
    //todo implement logging
    public class Queue : IQueue
    {
        private readonly string _queueName;
        private readonly int _queueType;
        private readonly SortedDictionary<int, Job> _queue = new SortedDictionary<int, Job>();

        public Queue(string name, int type)
        {
            //todo I dont like this implementation and we should have all jobs be in one queue. Lets index on both priority and on type instead

            _queueName = name;
            _queueType = type;
        }

        public void AddJob(Job job)
        {
            _queue.Add(GetNewId(), job);
        }

        public Job? Dequeue()
        {
            if (_queue.Count > 0)
            {
                KeyValuePair<int, Job> firstJob = _queue.First();
                _queue.Remove(firstJob.Key);
                return firstJob.Value;
            }
            else
            {
                return null;
            }
        }

        private int GetNewId()
        {
            if (_queue.Count == 0)
            {
                return 1;
            }
            else
            {
                int newId = _queue.Last().Key + 1;
                return newId;
            }
        }

        public string QueueName => _queueName;

        public int Type => _queueType;
    }
}
