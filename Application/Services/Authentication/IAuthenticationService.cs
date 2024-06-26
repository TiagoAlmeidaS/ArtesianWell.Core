using Application.Services.Authentication.Dtos;

namespace Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<SignInResponseDto> SignIn(SignInRequestDto requestDto);
    Task<SignUpResponseDto> SignUp(SignUpRequestDto requestDto);
}