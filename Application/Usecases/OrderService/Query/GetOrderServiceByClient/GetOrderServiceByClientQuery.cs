using MediatR;

namespace Application.Usecases.OrderService.Query.GetOrderServiceByClient;

public class GetOrderServiceByClientQuery : IRequest<List<GetOrderServiceByClientResult>>
{
    public string ClientId { get; set; }
}