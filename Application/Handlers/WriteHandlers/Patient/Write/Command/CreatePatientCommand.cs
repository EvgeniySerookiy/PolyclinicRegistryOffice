namespace PolyclinicRegistryOffice.Application.Handlers.WriteHandlers.Patient.Write.Command;

public record CreatePatientCommand(
    string LastName,
    string FirstName,
    string MiddleName,
    string PhoneNumber,
    string Email,
    DateTime DateOfBirth);