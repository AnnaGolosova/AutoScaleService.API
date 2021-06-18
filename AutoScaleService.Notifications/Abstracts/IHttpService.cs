using System.Threading.Tasks;

namespace AutoScaleService.Notifications.Abstracts
{
    public interface IHttpService
    {
        Task SendNotificationAsync(string url, object data);
    }
}
