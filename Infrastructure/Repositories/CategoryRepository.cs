using Application.Patterns;
using Dapper;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbSession _session;

        public CategoryRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Categories WHERE ID = @Id";
            return await _session.Connection.QuerySingleOrDefaultAsync<Category>(query, new { Id = id }, _session.Transaction);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var query = "SELECT * FROM Categories";
            return await _session.Connection.QueryAsync<Category>(query, _session.Transaction);
        }

        public async Task AddAsync(Category category)
        {
            var query = "INSERT INTO Categories (Name, Description) VALUES (@Name, @Description)";
            await _session.Connection.ExecuteAsync(query, category, _session.Transaction);
        }

        public void Update(Category category)
        {
            var query = "UPDATE Categories SET Name = @Name, Description = @Description WHERE ID = @ID";
            _session.Connection.Execute(query, category, _session.Transaction);
        }

        public void Delete(Category category)
        {
            var query = "DELETE FROM Categories WHERE ID = @ID";
            _session.Connection.Execute(query, new { category.ID }, _session.Transaction);
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            var query = "SELECT * FROM Categories WHERE Name = @name";
            return await _session.Connection.QuerySingleOrDefaultAsync<Category>(query, new { Name = name }, _session.Transaction);
        }
    }
}