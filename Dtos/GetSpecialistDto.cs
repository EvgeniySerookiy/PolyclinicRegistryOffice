namespace PolyclinicRegistryOffice.Dtos;

public record GetSpecialistDto(
    int Id,
    string LastName,
    string FirstName,
    string MiddleName,
    string OfficeNumber,
    string Description,
    string SpecializationName
);