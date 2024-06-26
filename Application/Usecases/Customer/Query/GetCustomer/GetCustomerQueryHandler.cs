using MediatR;

namespace Application.Usecases.Customer.Query.GetCustomer;

public class GetCustomerQueryHandler: IRequestHandler<GetCustomerQuery, GetCustomerResult>
{
    public Task<GetCustomerResult> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}