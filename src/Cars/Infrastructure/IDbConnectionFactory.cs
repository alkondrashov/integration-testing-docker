using System.Data;

namespace Cars.Infrastructure;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}