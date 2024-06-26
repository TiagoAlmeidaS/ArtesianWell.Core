using MediatR;

namespace Application.Usecases.Customer.Command.CreateCustomer;

public class CreateCustomerCommandHandler: IRequestHandler<CreateCustomerCommand, CreateCustomerResult>
{
    public Task<CreateCustomerResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}