namespace Application.Usecases.Authentication.Command.SignIn;

public class SignInResult
{
    public String Token { get; set; }
    public String RefreshToken { get; set; }
    public int TokenExpiration { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int ProfileType { get; set; }
}