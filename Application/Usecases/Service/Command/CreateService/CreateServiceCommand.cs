using FluentValidation;
using MediatR;

namespace Application.Usecases.Service.Command.CreateService;

public class CreateServiceCommand : IRequest<CreateServiceResult>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public bool Available { get; set; }
}

public class CreateServiceCommandAbstractValidator : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandAbstractValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Available).NotEmpty().WithMessage("Available is required");
    }
}