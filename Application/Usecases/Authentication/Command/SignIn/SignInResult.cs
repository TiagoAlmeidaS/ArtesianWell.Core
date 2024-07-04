namespace Application.Usecases.Authentication.Command.SignIn;

public class SignInResult
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public int TokenExpiration { get; set; }
}