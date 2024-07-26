using MediatR;

namespace Application.Usecases.OrderStatus.Query.GetOrderStatus;

public class GetOrderStatusQuery : IRequest<GetOrderStatusResult>
{
    public int OrderStatusId { get; set; }
    public string Status { get; set; }
}