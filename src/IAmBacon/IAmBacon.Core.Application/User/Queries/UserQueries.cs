using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using IAmBacon.Core.Application.Infrastructure;

namespace IAmBacon.Core.Application.User.Queries
{
    public class UserQueries : IUserQueries
    {
        private readonly IDbConnection _connection;

        public UserQueries(IDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            _connection = connectionFactory.CreateDbConnection();
        }

        public async Task<User> GetAsync(int id)
        {
            var result = await _connection.QueryAsync<User>(
                @"select Id, FirstName, Lastname, Email, Bio, ProfileImage, Active, Deleted from Users where id=@id and Deleted=0",
                new { id });

            var users = result.ToList();
            if (users.Count == 0)
            {
                throw new KeyNotFoundException();
            }

            return users.First();
        }

        public async Task<User> GetAsync(string email)
        {
            var result = await _connection.QueryAsync<User>(
                @"select FirstName, Lastname from Users where email=@email and Deleted=0", new { email });

            var users = result.ToList();
            if (users.Count == 0)
            {
                throw new KeyNotFoundException();
            }

            return users.First();
        }

        public async Task<IReadOnlyCollection<User>> GetAllAsync()
        {
            var result = await _connection.QueryAsync<User>(
                @"select Id, FirstName, LastName, DateCreated, DateModified, Active from Users where Active=1");

            return result.ToList();
        }
    }
}
