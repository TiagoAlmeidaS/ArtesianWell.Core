using MediatR;

namespace Application.Usecases.Authentication.Command.SignIn;

public class SignInCommand : IRequest<SignInResult>
{
    public string Key { get; set; }
    public string Password { get; set; }
    public string Code { get; set; }
}