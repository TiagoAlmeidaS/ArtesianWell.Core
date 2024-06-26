using MediatR;

namespace Application.Usecases.Customer.Command.UpdateCustomer;

public class UpdateCustomerCommandHandler: IRequestHandler<UpdateCustomerCommand, UpdateCustomerResult>
{
    public Task<UpdateCustomerResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}