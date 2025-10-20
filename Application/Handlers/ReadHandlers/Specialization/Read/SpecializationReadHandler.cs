using Dapper;
using PolyclinicRegistryOffice.Application.Interfaces.ReadInterfaces;
using PolyclinicRegistryOffice.Dtos;
using PolyclinicRegistryOffice.ErrorContext;
using PolyclinicRegistryOffice.Interfaces;

namespace PolyclinicRegistryOffice.Application.Handlers.ReadHandlers.Specialization.Read;

public class SpecializationReadHandler(ISqlConnectionFactory sqlConnectionFactory) : ISpecializationReadHandler
{
    public async Task<Result<IEnumerable<GetSpecializationDto>, Error>> GetSpecializations(
        CancellationToken cancellationToken)
    {
        try
        {
            using var connection = sqlConnectionFactory.Connection();

            const string sql = @"
                SELECT 
                    ""Id"",
                    ""Name""
                FROM ""Specializations""";

            var specializations = await connection.QueryAsync<GetSpecializationDto>(
                new CommandDefinition(sql, cancellationToken: cancellationToken)
            );

            return Result<IEnumerable<GetSpecializationDto>, Error>.Success(specializations);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<GetSpecializationDto>, Error>.Failure(
                Errors.Specialization.Database(ex.Message)
            );
        }
    }
}