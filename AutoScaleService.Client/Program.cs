using System;
using System.Net.Http;
using System.Text;

namespace AutoScaleService.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //var json = JsonConvert.SerializeObject(new object());
            var json = JsonConvert.
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://httpbin.org/post";
            using var client = new HttpClient();

            var responseTask = client.PostAsync(url, data);

            var response = responseTask.Result;

            string result = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine(result);
        }
    }
}
