using PolyclinicRegistryOffice.Dtos;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Application.Interfaces.ReadInterfaces;

public interface ISpecialistReadHandler
{
    Task<Result<IEnumerable<GetSpecialistDto>, Error>> GetSpecialists(
        CancellationToken cancellationToken);
    
    Task<Result<IEnumerable<GetSpecialistDto>, Error>> GetSpecialistsBySpecialization(
        int specializationId,
        CancellationToken cancellationToken);
}