using Application.Services.Authentication.Dtos;

namespace Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<SignInResponseDto> SignIn(SignInRequestDto requestDto, CancellationToken cancellationToken);
    Task<SignUpResponseDto> SignUp(SignUpRequestDto requestDto, CancellationToken cancellationToken);
}