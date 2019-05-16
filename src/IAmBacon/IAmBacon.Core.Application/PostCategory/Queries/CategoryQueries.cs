﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using IAmBacon.Core.Application.Infrastructure;

namespace IAmBacon.Core.Application.PostCategory.Queries
{
    public class CategoryQueries : ICategoryQueries
    {
        private readonly IDbConnection _connection;

        public CategoryQueries(IDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            _connection = connectionFactory.CreateDbConnection();
        }

        public async Task<Category> GetAsync(int id)
        {
            var result = await _connection.QueryAsync<Category>(
                @"select Id, Name, Active, Deleted from Categories where id=@id and Deleted=0", new {id});

            var categories = result.ToList();
            if (categories.Count == 0)
            {
                throw new KeyNotFoundException();
            }

            return categories.First();
        }

        public async Task<IReadOnlyCollection<Category>> GetAllAsync()
        {
            var result = await _connection.QueryAsync<Category>(@"select Id, Name from Categories where Active=1");

            return result.ToList();
        }
    }
}
