using FluentValidation;
using MediatR;

namespace Application.Usecases.Customer.Command.CreateCustomer;

public class CreateCustomerCommand: IRequest<CreateCustomerResult>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Document { get; set; }
    public string Phone { get; set; }
    public int ProfileType { get; set; }
}

public class CreateCustomerCommandAbstractValidator: AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandAbstractValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is required");
        RuleFor(x => x.Document).NotEmpty().WithMessage("Document is required");
        RuleFor(x => x.ProfileType).NotEmpty().WithMessage("ProfileType is required")
            .Must(x => x == 0 || x == 1).WithMessage("ProfileType must be either 0 or 1");;
    }
}