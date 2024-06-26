using MediatR;

namespace Application.Usecases.Authentication.Command.SignUp;

public class SignUpCommandHandler: IRequestHandler<SignUpCommand, SignUpResult>
{
    public Task<SignUpResult> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}