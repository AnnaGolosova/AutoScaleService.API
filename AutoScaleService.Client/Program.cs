using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using AutoScaleService.Models.Notifications;
using Newtonsoft.Json;
using RestSharp;

namespace AutoScaleService.Client
{
    class Program
    {
        private static readonly RestClient _client = new RestClient("http://localhost:33155");
        private static readonly Random _rnd = new Random();
        private static readonly string _listenUrl = "http://localhost:" + _rnd.Next(30000, 35000) + "/notifications/";

        static void Main(string[] args)
        {
            Task.Run(StartListenNotifications);

            while (true)
            {
                var request = new RestRequest("api/compute-resources", Method.POST);

                request.AddHeader("Accept", "application/json");

                var requestId = Guid.NewGuid();
                var translationTasksCount = _rnd.Next(1, 100000);

                var body = GetRequestBody(requestId, translationTasksCount);

                request.AddParameter("application/json", body, ParameterType.RequestBody);
                request.RequestFormat = DataFormat.Json;

                Console.WriteLine($"Send request with id: {requestId} to process {translationTasksCount} tasks \n");

                _client.Post(request);

                var secondsToSleep = _rnd.Next(1, 15);
                Thread.Sleep(secondsToSleep * 1000);

            }
        }

        private static string GetRequestBody(Guid id, int translationTasksCount)
        {
            var data = new
            {
                TranslationTasksCount = translationTasksCount,
                NotificationUrl = _listenUrl,
                RequestId = id
            };

            return JsonConvert.SerializeObject(data);
        }

        private static void StartListenNotifications()
        {

            using var listener = new HttpListener();
            listener.Prefixes.Add(_listenUrl);

            try
            {
                listener.Start();

                while (true)
                {
                    HttpListenerContext context = listener.GetContext();

                    var requestDataAsString = ParseNotificationRequestDataToString(context.Request);

                    var notification = JsonConvert.DeserializeObject<Notification>(requestDataAsString);

                    if (notification != null)
                    {
                        Console.WriteLine($"Notification received:\n" +
                                          $"RequestId {notification.RequestId} \n" +
                                          $"Message: {notification.ResultMessage} \n");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                listener.Stop();
            }
        }

        private static string ParseNotificationRequestDataToString(HttpListenerRequest request)
        {
            using Stream body = request.InputStream;
            using var reader = new StreamReader(body, request.ContentEncoding);

            return reader.ReadToEnd();
        }
    }
}
