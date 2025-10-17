using FluentValidation;
using PolyclinicRegistryOffice.Entities;

namespace PolyclinicRegistryOffice.Validators;

public class SpecialistValidator : AbstractValidator<Specialist>
{
    public SpecialistValidator()
    {
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        RuleFor(x => x.MiddleName)
            .MaximumLength(50).WithMessage("Middle name must not exceed 50 characters.");
        
        RuleFor(x => x.Specialization.Id) 
            .NotNull().WithMessage("Specialization ID is required.");

        RuleFor(x => x.OfficeNumber)
            .NotEmpty().WithMessage("Office number is required.")
            .MaximumLength(10).WithMessage("Office number must not exceed 10 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
    }
}