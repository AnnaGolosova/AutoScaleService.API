using System;

namespace AutoScaleService.Models.Notifications
{
    public class Notification
    {
        public Notification(Guid requestId, string resultMessage)
        {
            RequestId = requestId;
            ResultMessage = resultMessage;
        }

        public Notification()
        { }

        public Guid RequestId { get; set; }
        public string ResultMessage { get; set; }
    }
}
