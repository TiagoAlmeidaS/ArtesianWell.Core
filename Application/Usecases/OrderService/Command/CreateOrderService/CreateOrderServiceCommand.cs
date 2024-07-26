using FluentValidation;
using MediatR;

namespace Application.Usecases.OrderService.Command.CreateOrderService;

public class CreateOrderServiceCommand : IRequest<CreateOrderServiceResult>
{
    public string ClientId { get; set; }
    public DateTime BudgetSchedulingDate { get; set; }
    public string ServiceId { get; set; }
}

public class ValidateCreateOrderServiceCommand : AbstractValidator<CreateOrderServiceCommand>
{
    public ValidateCreateOrderServiceCommand()
    {
        RuleFor(x => x.ClientId).NotEmpty().WithMessage("ClientId is required.");
        RuleFor(x => x.BudgetSchedulingDate).NotEmpty().WithMessage("BudgetSchedulingDate is required.");
        RuleFor(x => x.ServiceId).NotEmpty().WithMessage("ServiceId is required.");
    }
}