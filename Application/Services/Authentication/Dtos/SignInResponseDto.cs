namespace Application.Services.Authentication.Dtos;

public class SignInResponseDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public int TokenExpiration { get; set; }
}

public class SignInRequestDto
{
    public string Key { get; set; }
    public string Password { get; set; }
    public string Code { get; set; }
}