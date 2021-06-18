using AutoScaleService.AbstractQueue;
using AutoScaleService.API.Data;
using AutoScaleService.API.Data.Abstracts;
using AutoScaleService.API.Services;
using AutoScaleService.API.Services.Abstracts;
using AutoScaleService.Notifications;
using AutoScaleService.Notifications.Abstracts;
using AutoScaleService.RabbitMq;
using Microsoft.Extensions.DependencyInjection;

namespace AutoScaleService.API.Extentions
{
    public static class StartupExtentions
    {
        public static void RequsterServices(this IServiceCollection services)
        {
            services.AddSingleton<ITasksQueue>(new RabbitMQTasksQueue());
            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<INotificationsService, NotificationsService>();
            services.AddTransient<IComputeResouncesManager, ComputeResouncesManager>();
            services.AddTransient<IComputeResourcesFactory, ComputeResourcesFactory>();
            services.AddSingleton<IResourcesStorage, ResourcesStorage>();
        }
    }
}
