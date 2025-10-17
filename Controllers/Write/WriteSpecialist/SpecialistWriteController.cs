using Microsoft.AspNetCore.Mvc;
using PolyclinicRegistryOffice.Application.Interfaces;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Controllers.Write.WriteSpecialist;

public class SpecialistWriteController(
    ISpecialistWriteHandler specialistWriteHandler) : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> CreateSpecialization(
        [FromBody] CreateSpecialistRequest request,
        CancellationToken cancellationToken)
    {
        var result = await specialistWriteHandler.CreateSpecialist(request.ToCommand(), cancellationToken);
        return result.ToActionResult();
    }
}