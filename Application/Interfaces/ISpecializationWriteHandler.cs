using Microsoft.AspNetCore.Mvc;
using PolyclinicRegistryOffice.Application.Handlers.Specialization.Write.Command;
using PolyclinicRegistryOffice.Entities;
using PolyclinicRegistryOffice.ErrorContext;

namespace PolyclinicRegistryOffice.Application.Interfaces;

public interface ISpecializationWriteHandler
{
    Task<Result<Specialization, Error>> CreateSpecialization(
        [FromBody] CreateSpecializationCommand command,
        CancellationToken cancellationToken);
}