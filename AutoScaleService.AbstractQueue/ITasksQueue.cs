namespace AutoScaleService.AbstractQueue
{
    public interface ITasksQueue<T> where T: class
    {
        bool TryPeekNextTask(out T task);
        bool TryGetNextTask(out T task);
        void TrySetNextTask(T task);

    }
}
