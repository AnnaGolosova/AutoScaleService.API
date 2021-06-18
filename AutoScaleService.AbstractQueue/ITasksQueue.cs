namespace AutoScaleService.AbstractQueue
{
    public interface ITasksQueue<T> where T: class
    {
        T PeekNextTask();
        T GetNextTask();
        void SetNextTask(T task);
    }
}
