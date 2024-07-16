using System.Net;
using Application.Services.Authentication;
using Application.Services.Authentication.Dtos;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.Authentication.Command.SignUp;

public class SignUpCommandHandler(IAuthenticationService service, IMessageHandlerService msg): IRequestHandler<SignUpCommand, SignUpResult>
{
    public async Task<SignUpResult> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var requestService = new SignUpRequestDto()
            {
                Document = request.Document,
                Email = request.Email,
                Name = request.Name,
                Password = request.Password,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber
            };
        
            var response = await service.SignUp(requestService, cancellationToken);
            
            if(response == null)
            {
                msg.AddError()
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .WithMessage(MessagesConsts.ErrorDefault)
                    .WithStatusCode(HttpStatusCode.BadRequest)
                    .Commit();
                
                return new();
            }
            
            return new();
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