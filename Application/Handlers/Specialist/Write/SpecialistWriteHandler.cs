using FluentValidation;
using PolyclinicRegistryOffice.Application.Handlers.Specialist.Write.Command;
using PolyclinicRegistryOffice.Application.Interfaces;
using PolyclinicRegistryOffice.Data;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Application.Handlers.Specialist.Write;

public class SpecialistWriteHandler(
    WriteDbContext context, 
    IValidator<Entities.Specialist> validator) : ISpecialistWriteHandler
{
    public async Task<Result<Entities.Specialist, Error>> CreateSpecialist(
        CreateSpecialistCommand command,
        CancellationToken cancellationToken)
    {
        var specialization = await context.Specializations
            .FindAsync(new object[] { command.SpecializationId }, cancellationToken);
        
        if (specialization == null)
        {
            return Result<Entities.Specialist, Error>.Failure(Errors.Specialization.NotFound(command.SpecializationId));
        }
        
        var specialist = new Entities.Specialist
        {
            LastName = command.LastName, 
            FirstName = command.FirstName, 
            MiddleName = command.MiddleName,
            Specialization = specialization,
            OfficeNumber = command.OfficeNumber,
            Description = command.Description
        };
        
        var validationResult = await validator.ValidateAsync(specialist, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<Entities.Specialist, Error>.Failure(ValidationErrorMapper.ToSingleError(validationResult.Errors));
        }
        
        context.Specialists.Add(specialist);
        await context.SaveChangesAsync(cancellationToken);
        
        return Result<Entities.Specialist, Error>.Success(specialist);
    }
}