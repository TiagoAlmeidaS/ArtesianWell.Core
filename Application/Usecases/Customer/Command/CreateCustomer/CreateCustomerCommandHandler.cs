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
        var response = await customerService.CreateCustomer(new());
        
        if(response == null)
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
            Name = response.Name,
            Email = response.Email
        };
    }
}