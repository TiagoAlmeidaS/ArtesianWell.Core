using System.Net;
using Application.Services.Customer;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.Customer.Command.CreateCustomer;

public class CreateCustomerCommandHandler(ICustomerService customerService, IMessageHandlerService messageHandlerService): IRequestHandler<CreateCustomerCommand, CreateCustomerResult>
{
    public async Task<CreateCustomerResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await customerService.CreateCustomer(new()
        {
            Email = request.Email,
            Number = request.Phone,
            Name = request.Name,
            Document = request.Document,
            ProfileType = request.ProfileType
        }, cancellationToken);
        
        if(response.HasError)
        {
            messageHandlerService.AddError()
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithMessage(MessagesConsts.ErrorDefault)
                .WithStatusCode(HttpStatusCode.BadRequest)
                .Commit();
            
            return new();
        }
        
        return new()
        {
            Name = response.Data.Name,
            Email = response.Data.Email,
            ProfileType = response.Data.ProfileType,
        };
    }
}