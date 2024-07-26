using MediatR;

namespace Application.Usecases.OrderService.Command.CompletedService;

public class CompletedServiceCommand : IRequest<CompletedServiceResult>
{
    public string OrderServiceId { get; set; }
}