using System.Net;
using System.Text.Json;
using Application.Services.Authentication;
using Application.Services.Authentication.Dtos;
using Authentication.Shared.Common;
using AutoMapper;
using Infra.Service.Clients.Authentication;
using Infra.Service.Clients.Authentication.Dto;
using Shared.Common;
using Shared.Dto;
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
            var loginResponse = JsonSerializer.Deserialize<ApiResponse<SignInResponseDto>>(responseContent, jsonSerializerOptions);
            return new()
            {
                Token = loginResponse.Data.Token,
                TokenExpiration = loginResponse.Data.TokenExpiration,
                RefreshToken = loginResponse.Data.RefreshToken
            };
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
            
            await TakeRegisterAuthenticationUser(bodyRequestAuthentication, cancellationToken);
            
            return new SignUpResponseDto();

        }
        catch (Exception e)
        {
            msg.AddError()
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithMessage(MessagesConsts.ErrorDefault)
                .WithStackTrace(e.StackTrace)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .Commit();

            return new SignUpResponseDto();
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