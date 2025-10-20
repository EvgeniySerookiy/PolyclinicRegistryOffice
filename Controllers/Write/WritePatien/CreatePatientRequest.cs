using PolyclinicRegistryOffice.Application.Handlers.WriteHandlers.Patient.Write.Command;

namespace PolyclinicRegistryOffice.Controllers.Write.WritePatien;

public record CreatePatientRequest(
    string LastName,
    string FirstName,
    string MiddleName,
    string PhoneNumber,
    string Email,
    DateTime DateOfBirth)
{
    public CreatePatientCommand ToCommand()
    {
        return new CreatePatientCommand(
            LastName, 
            FirstName, 
            MiddleName, 
            PhoneNumber, 
            Email, 
            DateOfBirth);
    }
}