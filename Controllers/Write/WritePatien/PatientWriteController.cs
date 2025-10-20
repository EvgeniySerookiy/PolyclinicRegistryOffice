using Microsoft.AspNetCore.Mvc;
using PolyclinicRegistryOffice.Application.Interfaces;
using PolyclinicRegistryOffice.Application.Interfaces.WriteInterfaces;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Controllers.Write.WritePatien;

public class PatientWriteController(
    IPatientWriteHandler patientWriteHandler) : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> CreateSpecialization(
        [FromBody] CreatePatientRequest request,
        CancellationToken cancellationToken)
    {
        var result = await patientWriteHandler.CreatePatient(request.ToCommand(), cancellationToken);
        return result.ToActionResult();
    }
}