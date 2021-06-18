using AutoScaleService.Notifications.Abstracts;
using AutoScaleService.Notifications.Models;
using System;
using System.Threading.Tasks;

namespace AutoScaleService.Notifications
{
    public class NotificationsService : INotificationsService
    {
        private readonly IHttpService _httpService;

        public NotificationsService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task SendNotification(Notification notification)
        {
            if(notification == null)
            {
                throw new ArgumentNullException(nameof(Notification));
            }

            var resultToSend = new 
            {
                Result = notification.Result,
                TaskId = notification.TaskId
            };

            await _httpService.SendNotificationAsync(notification.RedirectUrl, resultToSend);
        }
    }
}
