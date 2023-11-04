using Application.Patterns;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private IDbConnection _connection;

        public AuthorRepository(IDbConnection dbConnection)
        {
            _connection = dbConnection;
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Authors WHERE ID = @Id";
            return await _connection.QuerySingleOrDefaultAsync<Author>(query, new { Id = id });
        }

        public async Task<Author> GetByNameAsync(string lastName)
        {
            var query = "SELECT * FROM Authors WHERE LastName = @LastName";
            return await _connection.QuerySingleOrDefaultAsync<Author>(query, new { LastName = lastName });
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            var query = "SELECT * FROM Authors";
            return await _connection.QueryAsync<Author>(query);
        }

        public async Task AddAsync(Author author)
        {
            var query = "INSERT INTO Authors (FirstName, LastName) VALUES (@FirstName, @LastName)";
            await _connection.ExecuteAsync(query, author);
        }

        public async Task Update(Author author)
        {
            var query = "UPDATE Authors SET FirstName = @FirstName, LastName = @LastName WHERE ID = @ID";
            _connection.Execute(query, author);
        }

        public async Task Delete(Author author)
        {
            var query = "DELETE FROM Authors WHERE ID = @ID";
            _connection.Execute(query, new { author.ID });
        }
    }
}