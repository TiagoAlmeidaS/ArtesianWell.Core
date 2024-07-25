using FluentValidation;
using MediatR;

namespace Application.Usecases.Budget.Command.CreateBudget;

public class CreateBudgetCommand : IRequest<CreateBudgetResult>
{
    public string OrderServiceId { get; set; }
    public DateTime DateAccepted { get; set; } = DateTime.MinValue;
    public decimal TotalValue { get; set; }
    public string DescriptionService { get; set; }
    public DateTime DateChoose { get; set; } = DateTime.MinValue;
}

public class ValidateCreateBudgetCommand : AbstractValidator<CreateBudgetCommand>
{
    public ValidateCreateBudgetCommand()
    {
        RuleFor(x => x.OrderServiceId).NotEmpty().WithMessage("OrderServiceId is required.");
        RuleFor(x => x.TotalValue).NotEmpty().WithMessage("TotalValue is required.");
        RuleFor(x => x.DescriptionService).NotEmpty().WithMessage("DescriptionService is required.");
    }
}