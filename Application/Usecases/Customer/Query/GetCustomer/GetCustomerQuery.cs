using MediatR;

namespace Application.Usecases.Customer.Query.GetCustomer;

public class GetCustomerQuery : IRequest<GetCustomerResult>
{
    public string CustomerId { get; set; }   
}