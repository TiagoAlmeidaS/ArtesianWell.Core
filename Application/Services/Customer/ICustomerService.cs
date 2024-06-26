using Application.Services.Customer.Dto;

namespace Application.Services.Customer;

public interface ICustomerService
{
    Task<CreateCustomerResponse> CreateCustomer(CreateCustomerRequest request);
    Task<UpdateCustomerResponse> UpdateCustomer(UpdateCustomerRequest request);
    Task<GetCustomerResponse> GetCustomer(GetCustomerRequest request);
}