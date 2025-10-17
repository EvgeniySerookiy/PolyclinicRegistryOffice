using PolyclinicRegistryOffice.Application.Handlers.Patient.Write.Command;
using PolyclinicRegistryOffice.Entities;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Application.Interfaces;

public interface IPatientWriteHandler
{
    Task<Result<Patient, Error>> CreatePatient(
        CreatePatientCommand command, 
        CancellationToken cancellationToken);
}