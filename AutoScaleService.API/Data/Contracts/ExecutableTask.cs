using System;

namespace AutoScaleService.API.Data.Contracts
{
    public class ExecutableTask
    {
        public ExecutableTask(Guid id, string redirectUrl)
        {
            Id = id;
            TimeToExecute = 1;
            RedirectUrl = redirectUrl;
        }

        public readonly Guid Id;
        public readonly int TimeToExecute;
        public readonly string RedirectUrl;
    }
}
