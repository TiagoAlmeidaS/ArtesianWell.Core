using MediatR;

namespace Application.Usecases.OrderService.Command.CancelService;

public class CancelServiceCommand : IRequest<CancelServiceResult>
{
    public string OrderServiceId { get; set; }
}