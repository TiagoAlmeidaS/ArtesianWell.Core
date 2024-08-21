using FluentValidation;
using MediatR;
using Shared.Common;

namespace Application.Usecases.Schedules.Schedule.Query.GetSchedule;

public class GetScheduleQuery : IRequest<GetScheduleResult>
{
    public string Id { get; set; }
    public string UserId { get; set; }
}

public class ValidateGetScheduleQuery : AbstractValidator<GetScheduleQuery>
{
    public ValidateGetScheduleQuery()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(String.Format(MessagesConsts.ErrorRequired, "Id"));
        
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage(String.Format(MessagesConsts.ErrorRequired, "UserId"));
    }
}