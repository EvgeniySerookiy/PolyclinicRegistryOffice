using Microsoft.AspNetCore.Mvc;
using PolyclinicRegistryOffice.Application.Interfaces.ReadInterfaces;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Controllers.Read.ReadSpecialist;

public class SpecialistReadController(ISpecialistReadHandler specialistReadHandler) : ApplicationController
{
    [HttpGet]
    public async Task<ActionResult> GetSpecialists(
        CancellationToken cancellationToken)
    {
        var result = await specialistReadHandler.GetSpecialists(cancellationToken);

        return result.ToActionResult();
    }
    
    [HttpGet("{specializationId:int}")]
    public async Task<ActionResult> GetBySpecialization(
        int specializationId, 
        CancellationToken cancellationToken)
    {
        var result = await specialistReadHandler.GetSpecialistsBySpecialization(specializationId, cancellationToken);
        
        return result.ToActionResult();
    }
}