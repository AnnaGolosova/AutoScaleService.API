using AutoScaleService.Notifications.Models;
using System.Threading.Tasks;

namespace AutoScaleService.Notifications
{
    public interface INotificationsService
    {
        Task SendNotification(Notification notification);
    }
}
