using System;
using Newtonsoft.Json;
using RestSharp;

namespace AutoScaleService.Client
{
    class Program
    {
        private static readonly RestClient _client = new RestClient("http://localhost:33155");
        private static readonly Random rnd = new Random();

        static void Main(string[] args)
        {
            while (true)
            {
                var request = new RestRequest("api/compute-resources", Method.POST);

                request.AddHeader("Accept", "application/json");

                var task = GetTaskAsJson();

                request.AddParameter("application/json", task, ParameterType.RequestBody); 
                request.RequestFormat = DataFormat.Json;

                _client.Post(request);

                Console.ReadKey();
            }
        }

        private static string GetTaskAsJson()
        {
            var data = new
            {
                EstimatedTaskDuration = rnd.Next(1, 300),
                ComputeResourcesCount = rnd.Next(1, 10)
            };

            return JsonConvert.SerializeObject(data);
        }
    }
}
