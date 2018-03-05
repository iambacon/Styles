using System.Data;

namespace IAmBacon.Core.Application.Infrastructure
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateDbConnection();
    }
}
