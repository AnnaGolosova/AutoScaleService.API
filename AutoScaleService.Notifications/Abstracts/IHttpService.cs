using System.Threading.Tasks;

namespace AutoScaleService.Notifications.Abstracts
{
    public interface IHttpService
    {
        Task Post(string url, object data);
    }
}
