using MediatR;

namespace Application.Usecases.Service.Command.RemoveService;

public class RemoveServiceCommand : IRequest<RemoveServiceResult>
{
    public string ServiceId { get; set; }
}