using AutoScaleService.API.Data.Abstracts;
using AutoScaleService.API.Data.Contracts;
using AutoScaleService.API.Services.Abstracts;
using AutoScaleService.Models.Request;
using AutoScaleService.Models.ResourcesSettings;
using Microsoft.Extensions.Options;
using System;

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

        public void ProcessNextTask(RegisterTaskModel workItem)
        {
            if(workItem == null)
            {
                throw new ArgumentNullException();
            }

            _resourcesStorage.Execute(workItem);
        }

        public void ReleaseComputeResource(AbstractComputeResource computeResource)
            => _resourcesStorage.ReleaseComputeResource(computeResource);

        public bool CanProcessTask(int translationsCount)
            => _resourcesStorage.GetAvailableToCreateResourcesCount() >= translationsCount / _resourcesSettings.ResourceTranslationRate;
    }
}
