using FluentValidation;
using MediatR;

namespace Application.Usecases.OrderService.Query.GetOrderService;

public class GetOrderServiceQuery : IRequest<GetOrderServiceResult>
{
    public string OrderId { get; set; }
}

public class ValidateGetOrderServiceQuery : AbstractValidator<GetOrderServiceQuery>
{
    public ValidateGetOrderServiceQuery()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required.");
    }
}