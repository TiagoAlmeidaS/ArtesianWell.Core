namespace Application.Services.Customer.Dto;

public class GetCustomerResponse
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Document { get; set; }
    public string Number { get; set; }
    public int ProfileType { get; set; }
}

public class GetCustomerRequest
{
    public string Email { get; set; }
    public string Document { get; set; }
}