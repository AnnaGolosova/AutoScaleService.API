using MediatR;

namespace AutoScaleService.Application.Query
{
    public class GetAvailableResourcesCountQuery : IRequest<int>
    { }
}
