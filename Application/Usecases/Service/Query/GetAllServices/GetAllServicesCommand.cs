using MediatR;

namespace Application.Usecases.Service.Query.GetAllServices;

public class GetAllServicesCommand : IRequest<List<GetAllServicesResult>>
{
}