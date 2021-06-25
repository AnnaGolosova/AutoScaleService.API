using AutoScaleService.API.Data.Abstracts;
using AutoScaleService.API.Data.Contracts;
using AutoScaleService.API.Services.Abstracts;
using Microsoft.Extensions.Options;
using System;
using AutoScaleService.Models.Configuration;
using AutoScaleService.Models.Tasks;
using Serilog;

namespace AutoScaleService.API.Services
{
    public class ComputeResourcesManager : IComputeResourcesManager
    {
        private readonly IResourcesStorage _resourcesStorage;
        private readonly ResourcesSettings _resourcesSettings;

        public ComputeResourcesManager(IResourcesStorage resourcesStorage,
            IOptions<ResourcesSettings> resourcesSettings)
        {
            _resourcesStorage = resourcesStorage;
            _resourcesSettings = resourcesSettings.Value;
        }

        public void ProcessNextTask(RegisterTasksRequestDto tasksRequestDto)
        {
            if(tasksRequestDto == null)
            {
                throw new ArgumentNullException();
            }

            _resourcesStorage.Execute(tasksRequestDto);
        }

        public void ReleaseComputeResource(AbstractComputeResource computeResource)
        {
            Log.Logger.Information($"ExecutableTask with id {computeResource.ExecutableTask.Id} processed successfully");

            _resourcesStorage.ReleaseComputeResource(computeResource);
        }

        public bool CanProcessTask(int translationsCount)
        {
            return _resourcesStorage.GetAvailableToStartResourcesCount() + _resourcesStorage.GetIdleResourcesCount() >= translationsCount / _resourcesSettings.ResourceTranslationRate;
        }
    }
}
