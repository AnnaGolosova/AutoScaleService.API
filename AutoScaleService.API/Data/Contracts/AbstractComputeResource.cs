namespace AutoScaleService.API.Data.Contracts
{
    public abstract class AbstractComputeResource
    {
        public bool isBusy { get; private set; }

        public ExecutableTask Task { get; private set; }

        public AbstractComputeResource() { }

        public virtual void Invoke(ExecutableTask Task)
        {
            isBusy = true;

            System.Threading.Tasks.Task.Run(() => System.Threading.Thread.Sleep(Task.TimeToExecute));

            isBusy = false;
        }
    }
}
