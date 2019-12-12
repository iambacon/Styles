using System.Data;

namespace IAmBacon.Core.Application.Infrastructure.Fakes
{
    public class DapperDbConnectionFactoryFake : IDbConnectionFactory
    {
        public IDbConnection CreateDbConnection()
        {
            return new DbConnectionFake();
        }
    }
}
