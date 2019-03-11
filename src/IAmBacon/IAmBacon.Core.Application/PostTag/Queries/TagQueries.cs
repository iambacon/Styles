using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using IAmBacon.Core.Application.Infrastructure;

namespace IAmBacon.Core.Application.PostTag.Queries
{
    public class TagQueries : ITagQueries
    {
        private readonly IDbConnection _connection;

        public TagQueries(IDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            _connection = connectionFactory.CreateDbConnection();
        }


        public async Task<Tag> GetAsync(int id)
        {
            var result = await _connection.QueryAsync<Tag>(
                @"select Id, Name, Active, Deleted from Tags where id=@id and Deleted=0", new { id });

            var tags = result.ToList();
            if (tags.Count == 0)
            {
                throw new KeyNotFoundException();
            }

            return tags.First();
        }
    }
}
