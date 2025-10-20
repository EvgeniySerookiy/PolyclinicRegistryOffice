using Microsoft.AspNetCore.Mvc;
using PolyclinicRegistryOffice.Application.Interfaces.ReadInterfaces;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Controllers.Read.ReadSheduleSlot;

public class SheduleSlotReadController(
    IScheduleSlotReadHandler scheduleSlotReadHandler) : ApplicationController
{
    [HttpGet("{specialistId:int}/free-slots")]
    public async Task<ActionResult> GetFreeSlots(
        int specialistId,
        CancellationToken cancellationToken)
    {
        var result = await scheduleSlotReadHandler.GetFreeSlotsBySpecialist(specialistId, cancellationToken);

        return result.ToActionResult();
    }
}