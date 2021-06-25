using System;

namespace AutoScaleService.API.Data.Contracts
{
    public abstract class AbstractComputeResource
    {
        public bool IsBusy { get; private set; }

        public ExecutableTask ExecutableTask { get; set; }

        public Guid Id { get; }

        public string NotificationUrl { get; }

        protected AbstractComputeResource(string notificationUrl)
        {
            NotificationUrl = notificationUrl;
            Id = Guid.NewGuid();
        }

        public virtual void Invoke(ExecutableTask task)
        {
            IsBusy = true;

            System.Threading.Thread.Sleep(1000);

            IsBusy = false;
        }

        public virtual void Release()
        {
            IsBusy = false;
        }
    }
}
