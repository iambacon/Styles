using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using IAmBacon.Core.Application.Infrastructure;

namespace IAmBacon.Core.Application.Post.Queries
{
    public class PostQueries : IPostQueries
    {
        private readonly IDbConnection _connection;

        public PostQueries(IDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            _connection = connectionFactory.CreateDbConnection();
        }

        public async Task<Post> GetAsync(int id)
        {
            var result = await _connection.QueryAsync<Post>(@" select Id,
                                                                    AuthorId,
                                                                    CategoryId,
                                                                    Content,
                                                                    Image,
                                                                    Markdown,
                                                                    Title,
                                                                    NoCss,
                                                                    Active,
                                                                    Deleted from Posts where Id=@id and Deleted=0", new { id });

            var posts = result.ToList();
            if (posts.Count == 0)
            {
                throw new KeyNotFoundException();
            }

            return posts.First();
        }
    }
}
