using FluentValidation;
using MediatR;

namespace Application.Usecases.Authentication.Command.SignIn;

public class SignInCommand : IRequest<SignInResult>
{
    public String Key { get; set; }
    public String Password { get; set; }
    public String Code { get; set; }
}

public class SignInCommandAbstractValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandAbstractValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty()
            .WithMessage("Key is required");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required");

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Code is required");
    }
}