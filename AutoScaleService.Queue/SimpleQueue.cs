using AutoScaleService.AbstractQueue;
using System.Collections.Generic;

namespace AutoScaleService.Queue
{
    public class SimpleQueue<T> : ITasksQueue<T> where T : class
    {
        private readonly Queue<T> _storage = new Queue<T>();

        public T PeekNextTask()
        {
            //return _storage.Peek();

            return null;
        }

        public T GetNextTask()
        {
            return _storage.Dequeue();
        }

        public void SetNextTask(T task)
        {
            _storage.Enqueue(task);
        }
    }
}
