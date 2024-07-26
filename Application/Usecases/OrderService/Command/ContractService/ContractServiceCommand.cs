using MediatR;

namespace Application.Usecases.OrderService.Command.ContractService;

public class ContractServiceCommand : IRequest<ContractServiceResult>
{
    public string OrderServiceId { get; set; }
}