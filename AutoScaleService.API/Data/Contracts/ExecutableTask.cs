using System;

namespace AutoScaleService.API.Data.Contracts
{
    public class ExecutableTask
    {
        public ExecutableTask(Guid id, Guid requestId)
        {
            Id = id;
            RequestId = requestId;
            SecondsToExecute = 1;
        }

        public readonly Guid Id;
        public readonly int SecondsToExecute;
        public readonly Guid RequestId;
    }
}
