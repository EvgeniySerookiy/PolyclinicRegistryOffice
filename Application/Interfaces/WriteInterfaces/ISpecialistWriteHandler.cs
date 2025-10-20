using PolyclinicRegistryOffice.Application.Handlers.WriteHandlers.Specialist.Write.Command;
using PolyclinicRegistryOffice.Entities;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Application.Interfaces.WriteInterfaces;

public interface ISpecialistWriteHandler
{
    Task<Result<Specialist, Error>> CreateSpecialist(
        CreateSpecialistCommand command,
        CancellationToken cancellationToken);
}