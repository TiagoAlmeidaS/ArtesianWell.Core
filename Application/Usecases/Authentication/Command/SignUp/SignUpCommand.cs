using FluentValidation;
using MediatR;

namespace Application.Usecases.Authentication.Command.SignUp;

public class SignUpCommand : IRequest<SignUpResult>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Document { get; set; }
    public int ProfileType { get; set; }
}

public class SignUpCommandAbstractValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandAbstractValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("LastName is required");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("PhoneNumber is required");

        RuleFor(x => x.Document)
            .NotEmpty()
            .WithMessage("Document is required");
    }
}