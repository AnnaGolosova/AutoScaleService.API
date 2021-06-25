using AutoScaleService.Notifications.Abstracts;
using System;
using System.Threading.Tasks;
using AutoScaleService.Models.Notifications;

namespace AutoScaleService.Notifications
{
    public class NotificationsService : INotificationsService
    {
        private readonly IHttpService _httpService;

        public NotificationsService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task SendNotification(Notification notification, string url)
        {
            if(notification == null)
            {
                throw new ArgumentNullException(nameof(Notification));
            }

            await _httpService.Post(url, notification);
        }
    }
}
