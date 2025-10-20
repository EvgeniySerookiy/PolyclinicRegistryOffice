using PolyclinicRegistryOffice.Application.Handlers.WriteHandlers.ScheduleSlot.Write.Command;
using PolyclinicRegistryOffice.Dtos;
using PolyclinicRegistryOffice.Entities;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Application.Interfaces.WriteInterfaces;

public interface IScheduleSlotWriteHandler
{
    Task<Result<ScheduleSlot, Error>> CreateScheduleSlot(
        CreateScheduleSlotDto dto, 
        CancellationToken cancellationToken);
    
    Task<Result<List<ScheduleSlot>, Error>> CreateScheduleSlots(
        CreateScheduleSlotsCommand command, 
        CancellationToken cancellationToken);
}