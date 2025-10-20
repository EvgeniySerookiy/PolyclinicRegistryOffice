using System.Data;

namespace PolyclinicRegistryOffice.Interfaces;

public interface ISqlConnectionFactory
{
    IDbConnection Connection();
}