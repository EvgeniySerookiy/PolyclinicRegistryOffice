using Microsoft.AspNetCore.Mvc;
using PolyclinicRegistryOffice.Application.Interfaces;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Controllers.Write.WriteAppointment;

public class AppointmentWriteController(
    IAppointmentWriteHandler appointmentWriteHandler) : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> BookAppointment(
        [FromBody] BookAppointmentRequest request,
        CancellationToken cancellationToken)
    {
        var result = await appointmentWriteHandler.BookAppointment(request.ToCommand(), cancellationToken);
        return result.ToActionResult();
    }
}