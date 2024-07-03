namespace Infra.Service.Clients.Authentication.Dto;

public class RegisterJsonDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public string Name { get; set; }
    public int TokenExpiration { get; set; }
    public string Scope { get; set; }
}

public class RegisterJsonRequestDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string LastName { get; set; }
}