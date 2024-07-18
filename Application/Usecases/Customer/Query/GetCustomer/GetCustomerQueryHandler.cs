using System.Net;
using Application.Services.Customer;
using Application.Services.Customer.Dto;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.Customer.Query.GetCustomer;

public class GetCustomerQueryHandler(ICustomerService customerService, IMessageHandlerService msg): IRequestHandler<GetCustomerQuery, GetCustomerResult>
{
    public async Task<GetCustomerResult> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var requestService = new GetCustomerRequest()
            {
                Email = request.Email,
                Document = request.Document
            };
            
            var response = await customerService.GetCustomer(requestService, cancellationToken);
            if (response.HasError)
            {
                msg.AddError()
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .WithMessage(response.GetFirstErrorMessage())
                    .WithStatusCode((HttpStatusCode) response.GetFirtsErrorCode())
                    .Commit();

                return null;
            }

            return new GetCustomerResult()
            {
                Name = response.Data.Name,
                Email = response.Data.Email,
                Document = response.Data.Document,
                Number = response.Data.Number,
                ProfileType = response.Data.ProfileType
            };
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithMessage(MessagesConsts.ErrorDefault)
                .WithStatusCode(HttpStatusCode.BadRequest)
                .WithStackTrace(e.StackTrace)
                .Commit();
            
            throw;
        }
    }
}