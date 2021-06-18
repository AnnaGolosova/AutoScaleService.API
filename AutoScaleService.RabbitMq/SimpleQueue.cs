using System;
using System.Collections.Concurrent;
using AutoScaleService.AbstractQueue;

namespace AutoScaleService.RabbitMq
{
    public class SimpleQueue<T> : ITasksQueue<T> where T: class
    {
        private readonly ConcurrentQueue<T> _storage = new ConcurrentQueue<T>();

        public void AddTaskToQueue(T item)
        {
            _storage.Enqueue(item);
        }

        public bool TryDequeue(out T item)
        {
            var isSuccessful = _storage.TryDequeue(out item);

            return isSuccessful;
        }
    }
}
