using MediatR;

namespace Application.Usecases.OrderService.Command.ExecutingService;

public class ExecutingServiceCommand : IRequest<ExecutingServiceResult>
{
    public string OrderServiceId { get; set; }
}