using MediatR;

namespace Application.Usecases.OrderStatus.Query.GetAllOrderStatus;

public class GetAllOrderStatusQuery : IRequest<List<GetAllOrderStatusResult>>
{
}