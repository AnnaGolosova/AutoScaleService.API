namespace AutoScaleService.API.Data.Contracts
{
    public abstract class AbstractComputeResource
    {
        public bool isBusy { get; private set; }

        public ExecutableTask Task { get; set; }

        protected AbstractComputeResource() { }

        public virtual void Invoke(ExecutableTask task)
        {
            isBusy = true;

            System.Threading.Thread.Sleep(1000);

            isBusy = false;
        }

        public virtual void Release()
        {
            isBusy = false;
        }
    }
}
