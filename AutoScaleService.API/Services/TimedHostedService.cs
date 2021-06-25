using AutoScaleService.AbstractQueue;
using AutoScaleService.API.Services.Abstracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoScaleService.Models.Tasks;
using Serilog;

namespace AutoScaleService.API.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private Timer _timer;

        private readonly ITasksQueue<RegisterTasksRequestDto> _tasksQueue;
        private readonly IComputeResourcesManager _computeResourcesManager;

        public TimedHostedService(ILogger<TimedHostedService> logger,
            ITasksQueue<RegisterTasksRequestDto> tasksQueue,
            IComputeResourcesManager computeResourcesManager)
        {
            _tasksQueue = tasksQueue;
            _computeResourcesManager = computeResourcesManager;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            Log.Logger.Information("Timed Hosted Service running");

            _timer = new Timer(Execute, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        private void Execute(object state)
        {
            var isSuccessfulPeek = _tasksQueue.TryPeekNextTask(out RegisterTasksRequestDto tasksRequestDto);

            if (!isSuccessfulPeek)
            {
                return;
            }

            var taskCanBeProcessed = _computeResourcesManager.CanProcessTask(tasksRequestDto.TranslationTasksCount);

            if (!taskCanBeProcessed)
            {
                StopAsync(CancellationToken.None);

                return;
            }

            if (_tasksQueue.TryGetNextTask(out tasksRequestDto))
            {
                _computeResourcesManager.ProcessNextTask(tasksRequestDto);
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            
            Log.Logger.Information("Timed Hosted Service stopped.");

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        ~TimedHostedService()
        {
            _timer.Dispose();
        }
    }
}
