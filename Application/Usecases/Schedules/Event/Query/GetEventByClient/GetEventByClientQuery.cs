using MediatR;

namespace Application.Usecases.Schedules.Event.Query.GetEventByClient;

public class GetEventByClientQuery : IRequest<GetEventByClientResult>, IRequest<List<GetEventByClientResult>>
{
    public string UserId { get; set; }
}