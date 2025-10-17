using Microsoft.AspNetCore.Mvc;
using PolyclinicRegistryOffice.Application.Interfaces;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Controllers.Write.WriteSpecialization;

public class SpecializationWriteController (
    ISpecializationWriteHandler specializationWriteHandler) : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> CreateSpecialization(
        [FromBody] CreateSpecializationRequest request,
        CancellationToken cancellationToken)
    {
        var result = await specializationWriteHandler.CreateSpecialization(request.ToCommand(), cancellationToken);
        return result.ToActionResult();
    }
}