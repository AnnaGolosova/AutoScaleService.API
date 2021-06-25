using System.IO;
using AutoScaleService.AbstractQueue;
using AutoScaleService.API.Data;
using AutoScaleService.API.Data.Abstracts;
using AutoScaleService.API.Data.Contracts;
using AutoScaleService.API.Services;
using AutoScaleService.API.Services.Abstracts;
using AutoScaleService.Notifications;
using AutoScaleService.Notifications.Abstracts;
using AutoScaleService.Queue;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace AutoScaleService.API.Extensions
{
    public static class StartupExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddSingleton(typeof(ITasksQueue<>), typeof(SimpleQueue<>));
            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<INotificationsService, NotificationsService>();
            services.AddTransient<IComputeResourcesManager, ComputeResourcesManager>();
            services.AddTransient(typeof(IComputeResourcesFactory<ComputeResource>), typeof(ComputeResourcesFactory));
            services.AddSingleton<IResourcesStorage, ResourcesStorage>();
            services.AddSingleton<IHostedService, TimedHostedService>();
        }

        public static void UseFileLogging(this ILoggerFactory loggerFactory)
        {
            var currentDirectoryPath = Directory.GetCurrentDirectory();

            loggerFactory.AddFile($"{currentDirectoryPath}\\Logs\\log.txt");
        }
    }
}
