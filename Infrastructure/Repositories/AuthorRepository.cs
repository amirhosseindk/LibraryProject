using Application.Patterns;
using Dapper;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DbSession _session;

        public AuthorRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Authors WHERE ID = @Id";
            return await _session.Connection.QuerySingleOrDefaultAsync<Author>(query, new { Id = id }, _session.Transaction);
        }

        public async Task<Author> GetByNameAsync(string lastName)
        {
            var query = "SELECT * FROM Authors WHERE LastName = @LastName";
            return await _session.Connection.QuerySingleOrDefaultAsync<Author>(query, new { LastName = lastName }, _session.Transaction);
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            var query = "SELECT * FROM Authors";
            return await _session.Connection.QueryAsync<Author>(query, _session.Transaction);
        }

        public async Task AddAsync(Author author)
        {
            var query = "INSERT INTO Authors (FirstName, LastName) VALUES (@FirstName, @LastName)";
            await _session.Connection.ExecuteAsync(query, author , _session.Transaction);
        }

        public async Task Update(Author author)
        {
            var query = "UPDATE Authors SET FirstName = @FirstName, LastName = @LastName WHERE ID = @ID";
            _session.Connection.Execute(query, author, _session.Transaction);
        }

        public async Task Delete(Author author)
        {
            var query = "DELETE FROM Authors WHERE ID = @ID";
            _session.Connection.Execute(query, new { author.ID }, _session.Transaction);
        }
    }
}