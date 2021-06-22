using System.Collections.Concurrent;
using AutoScaleService.AbstractQueue;

namespace AutoScaleService.Queue
{
    public class SimpleQueue<T> : ITasksQueue<T> where T : class
    {
        private readonly ConcurrentQueue<T> _storage = new ConcurrentQueue<T>();

        public bool TryPeekNextTask(out T task)
        {
            return _storage.TryPeek(out task);
        }

        public bool TryGetNextTask(out T task)
        {
            return _storage.TryDequeue(out task);
        }

        public void TrySetNextTask(T task)
        {
            _storage.Enqueue(task);
        }
    }
}
