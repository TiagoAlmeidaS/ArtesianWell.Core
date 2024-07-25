namespace Infra.Service.Clients.Customer;

public class CustomerConst
{
    public static string GetApiName => "CustomerApi";   
    public static string GetPathCreateCustomer => "customer";
    public static string GetPathUpdateCustomer => "customer"; 
    public static string GetPathGetCustomer => "customer?document={0}&email={1}";
}