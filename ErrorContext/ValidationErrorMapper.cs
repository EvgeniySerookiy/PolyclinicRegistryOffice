using FluentValidation.Results;

namespace PolyclinicRegistryOffice.ErrorContext;

public static class ValidationErrorMapper
{
    public static Error ToSingleError(List<ValidationFailure> failures)
    {
        var errorMessages = string.Join("; ", 
            failures.Select(f => $"{f.PropertyName}: {f.ErrorMessage}"));

        return Error.Validation("validation.failed", errorMessages);
    }

    // public static List<Error> ToErrorList(List<ValidationFailure> failures)
    // {
    //     return failures
    //         .Select(f => Error.Validation(
    //             $"validation.{f.PropertyName.ToLower()}", f.ErrorMessage))
    //         .ToList();
    // }
}