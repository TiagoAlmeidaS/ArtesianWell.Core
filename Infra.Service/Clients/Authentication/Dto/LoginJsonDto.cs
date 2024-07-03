namespace Infra.Service.Clients.Authentication.Dto;

public class LoginJsonDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public int TokenExpiration { get; set; }
}