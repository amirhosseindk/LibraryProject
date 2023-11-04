using Application.Patterns;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private IDbConnection _connection;

        public CategoryRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Categories WHERE ID = @Id";
            return await _connection.QuerySingleOrDefaultAsync<Category>(query, new { Id = id });
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var query = "SELECT * FROM Categories";
            return await _connection.QueryAsync<Category>(query);
        }

        public async Task AddAsync(Category category)
        {
            var query = "INSERT INTO Categories (Name, Description) VALUES (@Name, @Description)";
            await _connection.ExecuteAsync(query, category);
        }

        public void Update(Category category)
        {
            var query = "UPDATE Categories SET Name = @Name, Description = @Description WHERE ID = @ID";
            _connection.Execute(query, category);
        }

        public void Delete(Category category)
        {
            var query = "DELETE FROM Categories WHERE ID = @ID";
            _connection.Execute(query, new { category.ID });
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            var query = "SELECT * FROM Categories WHERE Name = @name";
            return await _connection.QuerySingleOrDefaultAsync<Category>(query, new { Name = name });
        }
    }
}