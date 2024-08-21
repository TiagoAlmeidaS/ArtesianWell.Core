using FluentValidation;
using MediatR;

namespace Application.Usecases.Schedules.Schedule.Command.CreateSchedule;

public class CreateScheduleCommand : IRequest<CreateScheduleResult>
{
    public string UserId { get; set; }
}

public class ValidateCreateScheduleCommand : AbstractValidator<CreateScheduleCommand>
{
    public ValidateCreateScheduleCommand()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
    }
}