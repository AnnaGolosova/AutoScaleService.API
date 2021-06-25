using AutoScaleService.Models.Tasks;
using MediatR;

namespace AutoScaleService.API.Commands
{
    public class RegisterTaskCommand : IRequest
    {
        public RegisterTaskCommand(RegisterTasksRequestDto registerTasksRequest)
        {
            RegisterTasksRequest = registerTasksRequest;
        }

        public RegisterTasksRequestDto RegisterTasksRequest { get; set; }
    }
}
