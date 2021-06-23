using System;

namespace AutoScaleService.API.Data.Contracts
{
    public class WorkItem
    {
        public Guid TaskId { get; set; }
        public ExecutableTask Task { get; set; }
        public int TranslationTasksCount { get; set; }
    }
}
