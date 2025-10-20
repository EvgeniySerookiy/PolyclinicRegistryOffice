using Dapper;
using PolyclinicRegistryOffice.Application.Interfaces.ReadInterfaces;
using PolyclinicRegistryOffice.Dtos;
using PolyclinicRegistryOffice.ErrorContext;
using PolyclinicRegistryOffice.Interfaces;

namespace PolyclinicRegistryOffice.Application.Handlers.ReadHandlers.Specialist.Read;

public class SpecialistReadHandler(ISqlConnectionFactory sqlConnectionFactory) : ISpecialistReadHandler
{
    public async Task<Result<IEnumerable<GetSpecialistDto>, Error>> GetSpecialists(
        CancellationToken cancellationToken)
    {
        try
        {
            using var connection = sqlConnectionFactory.Connection();

            const string sql = @"
            SELECT 
                s.""Id"",
                s.""LastName"",
                s.""FirstName"",
                s.""MiddleName"",
                s.""OfficeNumber"",
                s.""Description"",
                sp.""Name"" AS ""SpecializationName""
            FROM ""Specialists"" s
            INNER JOIN ""Specializations"" sp ON s.""SpecializationId"" = sp.""Id""";

            var specialists = await connection.QueryAsync<GetSpecialistDto>(
                new CommandDefinition(sql, cancellationToken: cancellationToken)
            );

            return Result<IEnumerable<GetSpecialistDto>, Error>.Success(specialists);
        }
        catch (Exception ex)
        {
            
            return Result<IEnumerable<GetSpecialistDto>, Error>.Failure(Errors.Specialist.Database(ex.Message));
        }
    }

    public async Task<Result<IEnumerable<GetSpecialistDto>, Error>> GetSpecialistsBySpecialization(
        int specializationId,
        CancellationToken cancellationToken)
    {
        try
        {
            using var connection = sqlConnectionFactory.Connection();

            const string sql = @"
            SELECT 
                s.""Id"",
                s.""LastName"",
                s.""FirstName"",
                s.""MiddleName"",
                s.""OfficeNumber"",
                s.""Description"",
                sp.""Name"" AS ""SpecializationName""
            FROM ""Specialists"" s
            INNER JOIN ""Specializations"" sp ON s.""SpecializationId"" = sp.""Id""
            WHERE s.""SpecializationId"" = @SpecializationId";

            var specialists = await connection.QueryAsync<GetSpecialistDto>(
                new CommandDefinition(sql, new { SpecializationId = specializationId }, cancellationToken: cancellationToken)
            );

            return Result<IEnumerable<GetSpecialistDto>, Error>.Success(specialists);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<GetSpecialistDto>, Error>.Failure(
                Errors.Specialist.Database(ex.Message)
            );
        }
    }
}