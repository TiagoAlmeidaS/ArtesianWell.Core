namespace Application.Usecases.Authentication.Command.SignUp;

public class SignUpResult
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public int TokenExpiration { get; set; }
    public string Scope { get; set; }
}