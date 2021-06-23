using AutoScaleService.AbstractQueue;
using AutoScaleService.API.Data.Contracts;
using AutoScaleService.API.Services.Abstracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoScaleService.API.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedHostedService> _logger;
        private Timer _timer;

        private readonly ITasksQueue<WorkItem> _tasksQueue;
        private readonly IComputeResourcesManager _computeResourcesManager;

        public TimedHostedService(ILogger<TimedHostedService> logger,
            ITasksQueue<WorkItem> tasksQueue,
            IComputeResourcesManager computeResourcesManager)
        {
            _logger = logger;
            _tasksQueue = tasksQueue;
            _computeResourcesManager = computeResourcesManager;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var isSuccessfullPeek = _tasksQueue.TryPeekNextTask(out var task);

            if (!isSuccessfullPeek || !(task is WorkItem model) || model == null)
            {
                // There are no new messages in the queue
                return;
            }

            var taskCanBeProcessed = isSuccessfullPeek && _computeResourcesManager.CanProcessTask(model.TranslationTasksCount);

            if (!taskCanBeProcessed)
            {
                StopAsync(CancellationToken.None);
                return;
            }

            var isSuccessfullGet = _tasksQueue.TryGetNextTask(out task);

            if (isSuccessfullGet)
            {
                _computeResourcesManager.ProcessNextTask(task);
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
