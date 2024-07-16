using System.Text.Json;
using Application.Services.Customer;
using Application.Services.Customer.Dto;
using Shared.Messages;

namespace Infra.Service.Services;

public class CustomerService(IHttpClientFactory httpClient, JsonSerializerOptions jsonSerializerOptions, IMessageHandlerService service): ICustomerService
{
    public Task<CreateCustomerResponse> CreateCustomer(CreateCustomerRequest request)
    {
        return new Task<CreateCustomerResponse>(() => new CreateCustomerResponse()
        {
            Email = "teste@mock.com",
            Name = "Teste Mock",
            ProfileType = 0
        });
    }

    public Task<UpdateCustomerResponse> UpdateCustomer(UpdateCustomerRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<GetCustomerResponse> GetCustomer(GetCustomerRequest request)
    {
        throw new NotImplementedException();
    }
}