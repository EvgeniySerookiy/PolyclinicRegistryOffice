using PolyclinicRegistryOffice.Application.Handlers.Specialist.Write.Command;
using PolyclinicRegistryOffice.Entities;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Application.Interfaces;

public interface ISpecialistWriteHandler
{
    Task<Result<Specialist, Error>> CreateSpecialist(
        CreateSpecialistCommand command,
        CancellationToken cancellationToken);
}