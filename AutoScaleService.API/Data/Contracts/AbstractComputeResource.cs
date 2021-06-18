namespace AutoScaleService.API.Data.Contracts
{
    public abstract class AbstractComputeResource
    {
        public bool isBusy { get; private set; }

        public ExecutableTask Task { get; private set; }

        protected AbstractComputeResource() { }

        public virtual void Invoke(ExecutableTask task)
        {
            isBusy = true;

            System.Threading.Tasks.Task.Run(() => System.Threading.Thread.Sleep(task.TimeToExecute));

            isBusy = false;
        }

        public virtual void Release()
        {
            Task = null;
            isBusy = false;
        }
    }
}
