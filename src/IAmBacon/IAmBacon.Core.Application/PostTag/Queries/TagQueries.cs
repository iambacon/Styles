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

        public async Task<IReadOnlyCollection<Tag>> GetAllAsync()
        {
            var result = await _connection.QueryAsync<Tag>(@"select Id, Name from Tags where Active=1");

            return result.ToList();
        }

        public async Task<IReadOnlyCollection<Tag>> GetTagsForPost(int postId)
        {
            var result = await _connection.QueryAsync<Tag>(
                @"select Id, Name from Tags t left join PostTags pt on pt.TagId=t.Id where pt.PostId=@postId", new { postId });

            return result.ToList();
        }
    }
}
