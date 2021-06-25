using System.Threading.Tasks;
using AutoScaleService.Models.Notifications;

namespace AutoScaleService.Notifications.Abstracts
{
    public interface INotificationsService
    {
        Task SendNotification(Notification notification, string url);
    }
}
