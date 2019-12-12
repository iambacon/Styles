using System;
using System.Data;
using System.Data.SqlClient;

namespace IAmBacon.Core.Application.Infrastructure
{
    public class DapperDbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DapperDbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IDbConnection CreateDbConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
