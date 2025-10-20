using PolyclinicRegistryOffice.Dtos;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Application.Interfaces.ReadInterfaces;

public interface IScheduleSlotReadHandler
{
    Task<Result<IEnumerable<GetFreeSlotDto>, Error>> GetFreeSlotsBySpecialist(
        int specialistId,
        CancellationToken cancellationToken);

}