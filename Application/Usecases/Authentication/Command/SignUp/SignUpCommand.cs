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
}