namespace AutoScaleService.Models.Request
{
    public class RegisterTaskModel
    {
        /// <summary>
        /// Estimated time in milliseconds
        /// </summary>
        public int EstimatedTaskDuration { get; set; }

        /// <summary>
        /// Url of the client to send notification when task will be completed
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// How many computers will this task take
        /// </summary>
        public int ComputeResourcesCount { get; set; }
    }
}
