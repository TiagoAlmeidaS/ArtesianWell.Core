using MediatR;

namespace Application.Usecases.Customer.Command.UpdateCustomer;

public class UpdateCustomerCommand : IRequest<UpdateCustomerResult>
{
    public string CustomerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Document { get; set; }
    public string Number { get; set; }
    public int ProfileType { get; set; }
}