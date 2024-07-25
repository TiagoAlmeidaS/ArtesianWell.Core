using MediatR;

namespace Application.Usecases.Service.Query.GetService;

public class GetServiceCommand : IRequest<GetServiceResult>
{
    public string ServiceId { get; set; }
}