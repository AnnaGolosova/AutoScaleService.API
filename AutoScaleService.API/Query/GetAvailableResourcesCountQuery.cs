using MediatR;

namespace AutoScaleService.API.Query
{
    public class GetAvailableResourcesCountQuery : IRequest<int>
    { }
}
