namespace AutoScaleService.AbstractQueue
{
    public interface ITasksQueue
    {
        object PeekNextTask();
        object GetNextTask();
        void SetNextTask(object task);
    }
}
