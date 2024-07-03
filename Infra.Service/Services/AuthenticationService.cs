using System.Net;
using System.Text.Json;
using Application.Services.Authentication;
using Application.Services.Authentication.Dtos;
using Authentication.Shared.Common;
using AutoMapper;
using Infra.Service.Clients.Authentication;
using Infra.Service.Clients.Authentication.Dto;
using Shared.Common;
using Shared.Messages;
using Shared.Utils;

namespace Infra.Service.Services;

public class AuthenticationService(IHttpClientFactory httpClientFactory, JsonSerializerOptions jsonSerializerOptions, IMapper mapper, IMessageHandlerService msg): IAuthenticationService
{
    private HttpClient _httpClient = httpClientFactory.CreateClient(AuthenticationConst.GetAuthenticationNameApi);
    
    public async Task<SignInResponseDto> SignIn(SignInRequestDto requestDto, CancellationToken cancellationToken)
    {
        try
        {
            var body = HttpContentUtil.GetHttpContent(requestDto, ContentType.Json, jsonSerializerOptions);
            var response = await _httpClient.PostAsync(AuthenticationConst.GetLoginPath, body, cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                msg.AddError()
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .WithMessage(MessagesConsts.ErrorDefault)
                    .WithStackTrace(errorContent)
                    .WithStatusCode(response.StatusCode)
                    .Commit();
                
                return new();
            }
            
            var responseContent = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<SignInResponseDto>(responseContent, jsonSerializerOptions);
            return loginResponse;
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithMessage(MessagesConsts.ErrorDefault)
                .WithStackTrace(e.StackTrace)
                .WithStatusCode(HttpStatusCode.BadRequest)
                .Commit();
            
            throw;
        }
    }

    public async Task<SignUpResponseDto> SignUp(SignUpRequestDto requestDto, CancellationToken cancellationToken)
    {
        try
        {
            var bodyRequestAuthentication = new RegisterJsonRequestDto()
            {
                Email = requestDto.Email,
                Password = requestDto.Password,
                Name = requestDto.Name,
                LastName = requestDto.LastName
            };
            
            var responseRegisterAuthentication = await TakeRegisterAuthenticationUser(bodyRequestAuthentication, cancellationToken);
            
            var signUpResponse = new SignUpResponseDto()
            {
                Token = responseRegisterAuthentication.Token,
                RefreshToken = responseRegisterAuthentication.RefreshToken,
                Name = responseRegisterAuthentication.Name,
                TokenExpiration = responseRegisterAuthentication.TokenExpiration,
                Scope = responseRegisterAuthentication.Scope
            };

            return signUpResponse;

        }
        catch (Exception e)
        {
            msg.AddError()
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithMessage(MessagesConsts.ErrorDefault)
                .WithStackTrace(e.StackTrace)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .Commit();
            
            throw;
        }
    }

    #region Private Method

    private async Task<RegisterJsonDto> TakeRegisterAuthenticationUser(RegisterJsonRequestDto requestDto, CancellationToken cancellationToken)
    {
        try
        {
            var bodyRequestAuthenticationJson =
                HttpContentUtil.GetHttpContent(requestDto, ContentType.Json, jsonSerializerOptions);
            var response =
                await _httpClient.PostAsync(AuthenticationConst.GetRegisterPath, bodyRequestAuthenticationJson, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                msg.AddError()
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .WithMessage(MessagesConsts.ErrorDefault)
                    .WithStackTrace(errorContent)
                    .WithStatusCode(response.StatusCode)
                    .Commit();

                return new();
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var registerJsonRequestDto =
                JsonSerializer.Deserialize<RegisterJsonDto>(responseContent, jsonSerializerOptions);
            
            return registerJsonRequestDto;
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithMessage(MessagesConsts.ErrorDefault)
                .WithStackTrace(e.StackTrace)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .Commit();
            
            throw;
        }
    }

    #endregion
}