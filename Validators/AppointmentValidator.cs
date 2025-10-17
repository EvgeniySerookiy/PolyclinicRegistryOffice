using FluentValidation;
using PolyclinicRegistryOffice.Entities;

namespace PolyclinicRegistryOffice.Validators;

public class AppointmentValidator : AbstractValidator<Appointment>
{
    public AppointmentValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("Patient ID is required.");

        RuleFor(x => x.SpecialistId)
            .NotEmpty().WithMessage("Specialist ID is required.");

        RuleFor(x => x.AppointmentDate)
            .NotEmpty().WithMessage("Appointment date is required.")
            .Must(BeInFutureOrToday).WithMessage("Appointment date cannot be in the past.");
        
        RuleFor(x => x.AppointmentTime)
            .NotEmpty().WithMessage("Appointment time is required.");
    }
    
    private bool BeInFutureOrToday(DateTime date)
    {
        return date.Date >= DateTime.Today;
    }
}