using System;

namespace AutoScaleService.Notifications.Models
{
    public class Notification
    {
        public Guid TaskId { get; set; }
        public string RedirectUrl { get; set; }
        public object Result { get; set; }
    }
}
