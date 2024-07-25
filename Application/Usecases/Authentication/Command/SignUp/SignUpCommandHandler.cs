using System.Net;
using Application.Services.Authentication;
using Application.Services.Authentication.Dtos;
using Application.Usecases.Customer.Command.CreateCustomer;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.Authentication.Command.SignUp;

public class SignUpCommandHandler(IAuthenticationService service, IMessageHandlerService msg, IMediator mediator): IRequestHandler<SignUpCommand, SignUpResult>
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
            
            CreateCustomer(new()
            {
                Document = request.Document,
                Email = request.Email,
                Name = request.Name,
                Phone = request.PhoneNumber,
                ProfileType = ProfileType.Client
            }, cancellationToken);
            
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

    #region Private Methods

    async void CreateCustomer(CreateCustomerCommand createCustomerCommand, CancellationToken cancellationToken)
    {
        try
        {
            await mediator.Send(createCustomerCommand, cancellationToken);
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

    #endregion
}