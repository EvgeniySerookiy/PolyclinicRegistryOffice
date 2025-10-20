using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PolyclinicRegistryOffice.Application.Handlers.WriteHandlers.Patient.Write.Command;
using PolyclinicRegistryOffice.Application.Interfaces.WriteInterfaces;
using PolyclinicRegistryOffice.Data;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Application.Handlers.WriteHandlers.Patient.Write;

public class PatientWriteHandler(
    WriteDbContext context, 
    IValidator<Entities.Patient> validator) : IPatientWriteHandler
{
    public async Task<Result<Entities.Patient, Error>> CreatePatient(
        CreatePatientCommand command,
        CancellationToken cancellationToken)
    {
        var patient = new Entities.Patient
        {
            LastName = command.LastName,
            FirstName = command.FirstName,
            MiddleName = command.MiddleName,
            PhoneNumber = command.PhoneNumber,
            Email = command.Email,
            DateOfBirth = command.DateOfBirth
        };
        
        var validationResult = await validator.ValidateAsync(patient, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<Entities.Patient, Error>.Failure(
                ValidationErrorMapper.ToSingleError(validationResult.Errors)
            );
        }
        
        var exists = await context.Patients
            .AnyAsync(p => p.PhoneNumber == command.PhoneNumber, cancellationToken);

        if (exists)
        {
            return Result<Entities.Patient, Error>.Failure(
                Errors.Patient.AlreadyExists(command.PhoneNumber)
            );
        }
        
        // 4. Сохранение
        context.Patients.Add(patient);
        await context.SaveChangesAsync(cancellationToken);
        
        // 5. Успех
        return Result<Entities.Patient, Error>.Success(patient);
    }
}