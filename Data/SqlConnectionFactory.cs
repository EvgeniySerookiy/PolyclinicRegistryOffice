using System.Data;
using Npgsql;
using PolyclinicRegistryOffice.Interfaces;

namespace PolyclinicRegistryOffice.Data;

public class SqlConnectionFactory(string connectionString) :  ISqlConnectionFactory
{
    public IDbConnection Connection()
    {
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}