using System.Net;
using Application.Services.Authentication;
using Application.Services.Authentication.Dtos;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.Authentication.Command.SignIn;

public class SignInCommandHandler(IAuthenticationService authenticationService, IMessageHandlerService msg): IRequestHandler<SignInCommand, SignInResult>
{
    public async Task<SignInResult> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var signInRequest = new SignInRequestDto()
            {
                Code = request.Code,
                Password = request.Password,
                Key = request.Key
            };

            var response = await authenticationService.SignIn(signInRequest, cancellationToken);
            if(response == null)
            {
                msg.AddError()
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .WithMessage(MessagesConsts.ErrorDefault)
                    .WithStatusCode(HttpStatusCode.BadRequest)
                    .Commit();
                
                return new();
            }
             
            return new()
            {
                Token = response.Token,
                RefreshToken = response.RefreshToken,
                TokenExpiration = response.TokenExpiration
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
}