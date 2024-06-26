using MediatR;

namespace Application.Usecases.Authentication.Command.SignIn;

public class SignInCommandHandler: IRequestHandler<SignInCommand, SignInResult>
{
    public Task<SignInResult> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}