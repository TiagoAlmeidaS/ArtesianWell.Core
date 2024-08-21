using MediatR;

namespace Application.Usecases.Schedules.Event.Query.GetEvent;

public class GetEventQuery : IRequest<List<GetEventResult>>
{
    public string Id { get; set; }
    public List<string> Ids { get; set; }
}