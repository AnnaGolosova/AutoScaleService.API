namespace AutoScaleService.AbstractQueue
{
    public interface ITasksQueue<T>
    {
        void AddTaskToQueue(T item);
        bool TryDequeue(out T item);
    }
}
