using AutoScaleService.AbstractQueue;
using System;

namespace AutoScaleService.RabbitMq
{
    public class RabbitMQTasksQueue : ITasksQueue
    {
        public object GetNextTask()
        {
            throw new NotImplementedException();
        }

        public object PeekNextTask()
        {
            throw new NotImplementedException();
        }

        public void SetNextTask(object task)
        {
            throw new NotImplementedException();
        }
    }
}
