using System.Net;
using System.Text.Json;
using Application.Services.Customer;
using Application.Services.Customer.Dto;
using Authentication.Shared.Utils;
using Infra.Service.Clients.Customer;
using Shared.Dto;
using Shared.Messages;

namespace Infra.Service.Services;

public class CustomerService(IHttpClientFactory httpClientFactory, JsonSerializerOptions jsonSerializerOptions, IMessageHandlerService msg): ICustomerService
{
    private HttpClient _httpClient = httpClientFactory.CreateClient(CustomerConst.GetApiName);

    public async Task<ApiResponse<CreateCustomerResponse>> CreateCustomer(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var bodyContent = ContentsUtils.GetHttpContent(request, jsonSerializerOptions);
            var response =
                await _httpClient.PostAsync(CustomerConst.GetPathCreateCustomer, bodyContent, cancellationToken);
            
            var responseContent = await response.Content.ReadAsStringAsync();
            var customerResponse = JsonSerializer.Deserialize<ApiResponse<CreateCustomerResponse>>(responseContent, jsonSerializerOptions);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                msg.AddError()
                    .WithMessage($"Erro ao criar cliente: {customerResponse.GetFirstErrorMessage()}")
                    .WithStatusCode(response.StatusCode)
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .Commit();
                
                return ApiResponse<CreateCustomerResponse>.Error(new()
                {
                    ErrorCode = (int) response.StatusCode,
                    ErrorMessage = $"Erro ao criar cliente: {customerResponse.GetFirstErrorMessage()}"
                });
            }
            
            return customerResponse;
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithMessage($"Erro ao criar cliente: {e.Message}")
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithStackTrace(e.StackTrace)
                .Commit();
            
            throw;
        }
    }

    public Task<ApiResponse<UpdateCustomerResponse>> UpdateCustomer(UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<GetCustomerResponse>> GetCustomer(GetCustomerRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.GetAsync(String.Format(CustomerConst.GetPathGetCustomer, request.Document, request.Email), cancellationToken);
            
            var responseContent = await response.Content.ReadAsStringAsync();
            var customerResponse = JsonSerializer.Deserialize<ApiResponse<GetCustomerResponse>>(responseContent, jsonSerializerOptions);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = response.Content.ReadAsStringAsync();
                msg.AddError()
                    .WithMessage($"Erro ao buscar cliente: {errorContent.Result}")
                    .WithStatusCode(response.StatusCode)
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .Commit();
                
                return ApiResponse<GetCustomerResponse>.Error(new()
                {
                    ErrorCode = (int) response.StatusCode,
                    ErrorMessage = $"Erro ao buscar cliente: {errorContent.Result}"
                });
            }
            
            return customerResponse;
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithMessage($"Erro ao buscar cliente: {e.Message}")
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithStackTrace(e.StackTrace)
                .Commit();
            
            throw;
        }
    }
}