using System.Net;
using Application.Services.Authentication;
using Application.Services.Authentication.Dtos;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.Authentication.Command.SignIn;

public class SignInCommandHandler(IAuthenticationService service, IMessageHandlerService msg): IRequestHandler<SignInCommand, SignInResult>
{
    public async Task<SignInResult> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var requestService = new SignInRequestDto()
            {
                Key = request.Key,
                Password = request.Password,
                Code = request.Code
            };
        
            var response = await service.SignIn(requestService, cancellationToken);
            
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
                TokenExpiration = response.TokenExpiration,
                Name = "",
                Email = "",
                ProfileType = 0
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
            
            return new();
        }
    }
}