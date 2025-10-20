using PolyclinicRegistryOffice.Dtos;

namespace PolyclinicRegistryOffice.Application.Handlers.WriteHandlers.ScheduleSlot.Write.Command;

public record CreateScheduleSlotsCommand(
    List<CreateScheduleSlotDto> Slots);