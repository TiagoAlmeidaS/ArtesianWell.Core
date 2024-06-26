using System.Net.Http.Headers;
using System.Text.Json;
using Presentation.Dtos;
using Presentation.Models;

namespace Presentation.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class ClientsController : BaseController
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ClientsController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] Client cliente)
    {
        var client = _httpClientFactory.CreateClient();
        
        // Get admin token
        var tokenRequest = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", "admin-cli"),
            new KeyValuePair<string, string>("username", "admin"),
            new KeyValuePair<string, string>("password", "admin"),
            new KeyValuePair<string, string>("grant_type", "password")
        });

        var tokenResponse = await client.PostAsync("http://localhost:8080/realms/master/protocol/openid-connect/token", tokenRequest);
        if (!tokenResponse.IsSuccessStatusCode)
        {
            var errorContent = await tokenResponse.Content.ReadAsStringAsync();
            return BadRequest($"Erro ao obter token de admin: {errorContent}");
        }

        var token = JsonDocument.Parse(await tokenResponse.Content.ReadAsStringAsync()).RootElement.GetProperty("access_token").GetString();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Create user
        var userRequest = new
        {
            username = cliente.Email,
            email = cliente.Email,
            firstName = cliente.Name,
            lastName = cliente.Name,
            enabled = true,
            credentials = new[]
            {
                new
                {
                    type = "password",
                    value = cliente.Password,
                    temporary = false
                }
            }
        };

        var userResponse = await client.PostAsJsonAsync("http://localhost:8080/admin/realms/ArtesianWell/users", userRequest);
        if (!userResponse.IsSuccessStatusCode)
        {
            var errorContent = await userResponse.Content.ReadAsStringAsync();
            return BadRequest($"Erro ao registrar usuário no Keycloak: {errorContent}");
        }

        return Ok("Cliente registrado com sucesso");
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] ClientLoginDto loginDto)
    {
        var client = _httpClientFactory.CreateClient();
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", "artesianwell-client"),
            new KeyValuePair<string, string>("username", loginDto.Email),
            new KeyValuePair<string, string>("password", loginDto.Password),
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_secret", "Ugz4RQLOQhhcR5yYl8CcfIbcxEaZJPQE")
        });

        var keycloakResponse = await client.PostAsync("http://localhost:8080/realms/ArtesianWell/protocol/openid-connect/token", formContent);

        if (!keycloakResponse.IsSuccessStatusCode)
        {
            var errorContent = await keycloakResponse.Content.ReadAsStringAsync();
            return Unauthorized($"Credenciais inválidas: {errorContent}");
        }

        var responseContent = await keycloakResponse.Content.ReadAsStringAsync();
        return Ok(responseContent);
    }

    [HttpGet("me")]
    [Authorize] // Protege a rota, permitindo acesso apenas para usuários autenticados
    public IActionResult GetMe()
    {
        var cliente = new Client
        {
            Name = User.Identity.Name,
            Email = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value
        };

        return Ok(cliente);
    }
}
