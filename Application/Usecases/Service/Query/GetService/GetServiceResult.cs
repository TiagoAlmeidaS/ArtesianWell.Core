namespace Application.Usecases.Service.Query.GetService;

public class GetServiceResult
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool Available { get; set; }
}