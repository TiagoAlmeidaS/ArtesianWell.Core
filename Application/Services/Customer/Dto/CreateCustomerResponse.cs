namespace Application.Services.Customer.Dto;

public class CreateCustomerResponse
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Document { get; set; }
    public string Number { get; set; }
    public int ProfileType { get; set; }
}

public class CreateCustomerRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Document { get; set; }
    public string Number { get; set; }
    public int ProfileType { get; set; }
}