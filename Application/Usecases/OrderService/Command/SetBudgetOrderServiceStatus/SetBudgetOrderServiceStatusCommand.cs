using FluentValidation;
using MediatR;

namespace Application.Usecases.OrderService.Command.SetBudgetOrderServiceStatus;

public class SetBudgetOrderServiceStatusCommand : IRequest<SetBudgetOrderServiceStatusResult>
{
    public string OrderServiceId { get; set; }
}

public class ValidateSetBudgetOrderServiceStatusCommand : AbstractValidator<SetBudgetOrderServiceStatusCommand>
{
    public ValidateSetBudgetOrderServiceStatusCommand()
    {
        RuleFor(x => x.OrderServiceId).NotEmpty();
    }
}

