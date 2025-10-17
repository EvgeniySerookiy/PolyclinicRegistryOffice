using FluentValidation;
using PolyclinicRegistryOffice.Entities;

namespace PolyclinicRegistryOffice.Validators;

public class SpecializationValidator : AbstractValidator<Specialization>
{
    public SpecializationValidator()
    {
        RuleFor(s => s.Name)
            .NotEmpty().WithMessage("Specialization name is required.")
            .MaximumLength(100).WithMessage("Specialization name must not exceed 100 characters.");
    }
}