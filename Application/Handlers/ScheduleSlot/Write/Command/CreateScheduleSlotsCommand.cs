using PolyclinicRegistryOffice.Dtos;

namespace PolyclinicRegistryOffice.Application.Handlers.ScheduleSlot.Write.Command;

public record CreateScheduleSlotsCommand(
    List<CreateScheduleSlotDto> Slots);