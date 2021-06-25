using System.Threading;
using System.Threading.Tasks;
using AutoScaleService.AbstractQueue;
using AutoScaleService.API.Commands;
using AutoScaleService.Models.Configuration;
using AutoScaleService.Models.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AutoScaleService.API.CommandHandlers
{
    public class RegisterTaskCommandHandler : IRequestHandler<RegisterTaskCommand>
    {
        private readonly ITasksQueue<RegisterTasksRequestDto> _tasksQueue;
        private readonly ResourcesSettings _resourcesSettings;
        private readonly ILogger<RegisterTaskCommandHandler> _logger;

        public RegisterTaskCommandHandler(ITasksQueue<RegisterTasksRequestDto> tasksQueue, IOptions<ResourcesSettings> resourcesSettings, ILogger<RegisterTaskCommandHandler> logger)
        {
            _tasksQueue = tasksQueue;
            _logger = logger;
            _resourcesSettings = resourcesSettings.Value;
        }

        public Task<Unit> Handle(RegisterTaskCommand request, CancellationToken cancellationToken)
        {
            RegisterTasksRequestDto registerTaskModel = request.RegisterTasksRequest;

            _logger.LogInformation($"Get request to execute {registerTaskModel.TranslationTasksCount} tasks");

            var fullTasksCount = registerTaskModel.TranslationTasksCount / _resourcesSettings.RequiredRate;

            for (int i = 0; i < fullTasksCount; i++)
            {
                _tasksQueue.TrySetNextTask(new RegisterTasksRequestDto(_resourcesSettings.RequiredRate, registerTaskModel.NotificationUrl, registerTaskModel.RequestId));
            }

            var remainingTasks = registerTaskModel.TranslationTasksCount % _resourcesSettings.RequiredRate;

            if (remainingTasks != default)
            {
                _tasksQueue.TrySetNextTask(new RegisterTasksRequestDto(remainingTasks, registerTaskModel.NotificationUrl, registerTaskModel.RequestId));
            }

            return Unit.Task;
        }
    }
}
