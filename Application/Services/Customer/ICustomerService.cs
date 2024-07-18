using Application.Services.Customer.Dto;
using Shared.Dto;

namespace Application.Services.Customer;

public interface ICustomerService
{
    Task<ApiResponse<CreateCustomerResponse>> CreateCustomer(CreateCustomerRequest request, CancellationToken cancellationToken);
    Task<ApiResponse<UpdateCustomerResponse>> UpdateCustomer(UpdateCustomerRequest request, CancellationToken cancellationToken);
    Task<ApiResponse<GetCustomerResponse>> GetCustomer(GetCustomerRequest request, CancellationToken cancellationToken);
}