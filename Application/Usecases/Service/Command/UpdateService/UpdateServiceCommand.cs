using MediatR;

namespace Application.Usecases.Service.Command.UpdateService;

public class UpdateServiceCommand : IRequest<UpdateServiceResult>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Available { get; set; }
}