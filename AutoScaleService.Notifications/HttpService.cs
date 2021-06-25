using AutoScaleService.Notifications.Abstracts;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutoScaleService.Notifications
{
    public class HttpService : IHttpService
    {
        public async Task Post(string url, object data)
        {
            HttpClient httpClient = new HttpClient();

            StringContent body = new StringContent(JsonConvert.SerializeObject(data));

            await httpClient.PostAsync(url, body);
        }
    }
}
