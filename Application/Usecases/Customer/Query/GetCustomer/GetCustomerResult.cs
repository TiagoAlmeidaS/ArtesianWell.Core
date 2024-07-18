namespace Application.Usecases.Customer.Query.GetCustomer;

public class GetCustomerResult
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Document { get; set; }
    public string Number { get; set; }
    public int ProfileType { get; set; }
}