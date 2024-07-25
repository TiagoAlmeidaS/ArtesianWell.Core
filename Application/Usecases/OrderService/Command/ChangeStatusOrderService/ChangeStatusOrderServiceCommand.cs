using FluentValidation;
using MediatR;

namespace Application.Usecases.OrderService.Command.ChangeStatusOrderService;

public class ChangeStatusOrderServiceCommand : IRequest<ChangeStatusOrderServiceResult>
{
    public string Id { get; set; }
    public string Status { get; set; }
}

public class ValidateChangeStatusOrderServiceCommand : AbstractValidator<ChangeStatusOrderServiceCommand>
{
    public ValidateChangeStatusOrderServiceCommand()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Status).NotEmpty();
    }
}