using PolyclinicRegistryOffice.Dtos;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Application.Interfaces.ReadInterfaces;

public interface ISpecializationReadHandler
{
    Task<Result<IEnumerable<GetSpecializationDto>, Error>> GetSpecializations(
        CancellationToken cancellationToken);
}