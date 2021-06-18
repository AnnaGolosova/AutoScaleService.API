using System.Threading;
using System.Threading.Tasks;
using AutoScaleService.API.Data.Abstracts;
using AutoScaleService.API.Query;
using MediatR;

namespace AutoScaleService.API.QueryHandlers
{
    public class GetAvailableResourcesCountQueryHandler : IRequestHandler<GetAvailableResourcesCountQuery, int>
    {
        private readonly IResourcesStorage _resourcesStorage;

        public GetAvailableResourcesCountQueryHandler(IResourcesStorage resourcesStorage)
        {
            _resourcesStorage = resourcesStorage;
        }

        public async Task<int> Handle(GetAvailableResourcesCountQuery request, CancellationToken cancellationToken)
        {
            return _resourcesStorage.GetAvailableResourcesCount();
        }
    }
}
