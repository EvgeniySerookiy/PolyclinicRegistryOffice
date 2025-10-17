using PolyclinicRegistryOffice.Application.Handlers.Specialist.Write.Command;

namespace PolyclinicRegistryOffice.Controllers.Write.WriteSpecialist;

public record CreateSpecialistRequest(
    string LastName,
    string FirstName,
    string MiddleName,
    int SpecializationId,
    string OfficeNumber,
    string Description)
{
    public CreateSpecialistCommand ToCommand()
    {
        return new CreateSpecialistCommand(
            LastName, 
            FirstName, 
            MiddleName, 
            SpecializationId, 
            OfficeNumber, 
            Description);
    }
}