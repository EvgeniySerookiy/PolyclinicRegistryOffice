using FluentValidation;
using PolyclinicRegistryOffice.Entities;

namespace PolyclinicRegistryOffice.Validators;

public class FeedbackValidator : AbstractValidator<Feedback>
{
    public FeedbackValidator()
    {
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message text is required.")
            .MaximumLength(2000).WithMessage("Message must not exceed 2000 characters.");
            
        // Date is auto-generated, so we only check if it's not a default value (optional)
        RuleFor(x => x.Date)
            .NotEqual(default(DateTime)).WithMessage("Date must be a valid date.");
    }
}