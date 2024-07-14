using Queue;

namespace JobFactory
{
    public interface IJobCreator
    {
        void AddNewJobsToQueue(IQueue queue);

        void AddSingleJob(IQueue queue, String name, int queuetype, int priority, int runtime);
    }
}