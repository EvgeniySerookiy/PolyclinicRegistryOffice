using PolyclinicRegistryOffice.Application.Handlers.WriteHandlers.Specialization.Write.Command;

namespace PolyclinicRegistryOffice.Controllers.Write.WriteSpecialization;

public record CreateSpecializationRequest(
    string Name)
{
    public CreateSpecializationCommand ToCommand()
    {
        return new(Name);
    }
}