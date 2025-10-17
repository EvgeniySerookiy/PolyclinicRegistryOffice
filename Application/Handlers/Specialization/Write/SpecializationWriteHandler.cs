using FluentValidation;
using PolyclinicRegistryOffice.Application.Handlers.Specialization.Write.Command;
using PolyclinicRegistryOffice.Application.Interfaces;
using PolyclinicRegistryOffice.Data;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Application.Handlers.Specialization.Write;

public class SpecializationWriteHandler(
    WriteDbContext context, 
    IValidator<Entities.Specialization> validator) : ISpecializationWriteHandler
{
    public async Task<Result<Entities.Specialization, Error>> CreateSpecialization(
        CreateSpecializationCommand command, 
        CancellationToken cancellationToken)
    {
        var specialization = new Entities.Specialization { Name = command.Name };
        
        var validationResult = await validator.ValidateAsync(specialization, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<Entities.Specialization, Error>.Failure(ValidationErrorMapper.ToSingleError(validationResult.Errors));
        }
        
        context.Specializations.Add(specialization);
        await context.SaveChangesAsync(cancellationToken);
        
        return Result<Entities.Specialization, Error>.Success(specialization);
    }
    
    // public async Task<Result<Specialization, Error>> DeleteSpecialization(
    //     CreateSpecializationCommand command, 
    //     CancellationToken cancellationToken)
    // {
    //     var specialization = new Specialization { Name = command.Name };
    //     
    //     var validationResult = await validator.ValidateAsync(specialization, cancellationToken);
    //     if (!validationResult.IsValid)
    //     {
    //         return Result<Specialization, Error>.Failure(ValidationErrorMapper.ToSingleError(validationResult.Errors));
    //     }
    //     
    //     context.Specializations.Add(specialization);
    //     await context.SaveChangesAsync(cancellationToken);
    //     
    //     return Result<Specialization, Error>.Success(specialization);
    // }
}