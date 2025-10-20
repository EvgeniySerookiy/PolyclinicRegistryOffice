using Microsoft.AspNetCore.Mvc;
using PolyclinicRegistryOffice.Application.Interfaces;
using PolyclinicRegistryOffice.Application.Interfaces.WriteInterfaces;
using PolyclinicRegistryOffice.Dtos;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Controllers.Write.WriteScheduleSlot;

public class ScheduleSlotWriteController(
    IScheduleSlotWriteHandler scheduleSlotWriteHandler) : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> CreateScheduleSlot(
        [FromBody] CreateScheduleSlotDto request,
        CancellationToken cancellationToken)
    {
        var result = await scheduleSlotWriteHandler.CreateScheduleSlot(request, cancellationToken);
        return result.ToActionResult();
    }
    
    [HttpPost("bulk")]
    public async Task<ActionResult> CreateScheduleSlots(
        [FromBody] CreateScheduleSlotsRequest request,
        CancellationToken cancellationToken)
    {
        var result = await scheduleSlotWriteHandler.CreateScheduleSlots(request.ToCommand(), cancellationToken);
        return result.ToActionResult();
    }
    
}