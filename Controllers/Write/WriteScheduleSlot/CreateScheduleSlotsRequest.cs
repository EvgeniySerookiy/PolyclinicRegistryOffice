using PolyclinicRegistryOffice.Application.Handlers.ScheduleSlot.Write.Command;
using PolyclinicRegistryOffice.Dtos;

namespace PolyclinicRegistryOffice.Controllers.Write.WriteScheduleSlot;

public record CreateScheduleSlotsRequest(
    List<CreateScheduleSlotDto> Slots)
{
    public CreateScheduleSlotsCommand ToCommand()
    {
        return new CreateScheduleSlotsCommand(Slots);
    }
}