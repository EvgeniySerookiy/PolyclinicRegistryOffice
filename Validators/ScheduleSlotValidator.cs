using FluentValidation;
using PolyclinicRegistryOffice.Entities;

namespace PolyclinicRegistryOffice.Validators;

public class ScheduleSlotValidator : AbstractValidator<ScheduleSlot>
{
    public ScheduleSlotValidator()
    {
        RuleFor(x => x.SpecialistId)
            .NotEmpty().WithMessage("Specialist ID is required.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.")
            .Must(BeInFutureOrToday).WithMessage("The schedule date cannot be in the past.");
        
        RuleFor(x => x.DurationMinutes)
            .InclusiveBetween(5, 120).WithMessage("Duration must be between 5 and 120 minutes.");

        RuleFor(x => x.StartTime)
            .LessThan(x => x.EndTime).WithMessage("Start time must be before end time.");
    }

    private bool BeInFutureOrToday(DateTime date)
    {
        return date.Date >= DateTime.Today;
    }
}