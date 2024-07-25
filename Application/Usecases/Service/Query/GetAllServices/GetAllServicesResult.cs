namespace Application.Usecases.Service.Query.GetAllServices;

public class GetAllServicesResult
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool Available { get; set; }
}