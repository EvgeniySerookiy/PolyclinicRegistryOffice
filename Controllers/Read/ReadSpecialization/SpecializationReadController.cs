using Microsoft.AspNetCore.Mvc;
using PolyclinicRegistryOffice.Application.Interfaces.ReadInterfaces;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Controllers.Read.ReadSpecialization;

public class SpecializationReadController(ISpecializationReadHandler specializationReadHandler) : ApplicationController
{
    [HttpGet]
    public async Task<ActionResult> GetSpecializations(
        CancellationToken cancellationToken)
    {
        var result = await specializationReadHandler.GetSpecializations(cancellationToken);

        return result.ToActionResult();
    }
}