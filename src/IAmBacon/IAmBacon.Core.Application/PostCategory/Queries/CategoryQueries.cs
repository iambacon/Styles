using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace IAmBacon.Core.Application.PostCategory.Queries
{
    public class CategoryQueries : ICategoryQueries
    {
        private readonly string _connectionString;

        public CategoryQueries(string connectionString)
        {
            _connectionString = !string.IsNullOrWhiteSpace(connectionString)
                ? connectionString
                : throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<Category> GetAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<Category>(@"select Id, Name, Active from Categories where id=@id", new { id });

                var categories = result.ToList();
                if (categories.Count == 0)
                {
                    throw new KeyNotFoundException();
                }

                return categories.First();
            }
        }
    }
}
