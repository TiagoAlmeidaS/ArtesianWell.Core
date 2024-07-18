using FluentValidation;
using MediatR;

namespace Application.Usecases.Customer.Query.GetCustomer;

public class GetCustomerQuery : IRequest<GetCustomerResult>
{
    public string Email { get; set; }
    public string Document { get; set; }
}

public class GetCustomerAbstractValidator : AbstractValidator<GetCustomerQuery>
{
    public GetCustomerAbstractValidator()
    {
        RuleFor(x => x).Must(x => !string.IsNullOrEmpty(x.Document) || !string.IsNullOrEmpty(x.Email))
            .WithMessage("Either Document or Email is required");
    }
}