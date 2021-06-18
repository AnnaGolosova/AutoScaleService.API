using System;

namespace AutoScaleService.API.Data.Contracts
{
    public class ExecutableTask
    {
        public ExecutableTask(Guid id, int timeToExecute, int redirectUrl)
        {
            Id = id;
            TimeToExecute = timeToExecute;
            RedirectUrl = redirectUrl;
        }

        public readonly Guid Id;
        public readonly int TimeToExecute;
        public readonly int RedirectUrl;
    }
}
