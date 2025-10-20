using PolyclinicRegistryOffice.Dtos;

namespace PolyclinicRegistryOffice.Application.Handlers.ReadHandlers.Specialist.Read.Query;

public record GetSpecialistsQuery(
    IEnumerable<GetSpecialistDto> Specialists);