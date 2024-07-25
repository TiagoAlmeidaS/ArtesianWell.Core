using MediatR;

namespace Application.Usecases.OrderService.Command.SetServiceScheduleDate;

public class SetServiceScheduleDateCommand : IRequest<SetServiceScheduleDateResult>
{
    public string OrderServiceId { get; set; }
    public DateTime ServiceSchedulingDate { get; set; }
}