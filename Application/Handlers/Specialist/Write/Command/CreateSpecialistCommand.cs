namespace PolyclinicRegistryOffice.Application.Handlers.Specialist.Write.Command;

public record CreateSpecialistCommand(
    string LastName,
    string FirstName,
    string MiddleName,
    int SpecializationId,
    string OfficeNumber,
    string Description);