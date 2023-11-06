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

        public async Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var query = "SELECT * FROM Categories WHERE ID = @Id";
            return await _session.Connection.QueryFirstOrDefaultAsync<Category>(
                new CommandDefinition(
                    query,
                    new { Id = id },
                    _session.Transaction,
                    cancellationToken: cancellationToken
                    )
                ).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var query = "SELECT * FROM Categories";
            return await _session.Connection.QueryAsync<Category>(
                new CommandDefinition(
                    query,
                    _session.Transaction,
                    cancellationToken:cancellationToken
                    )
                ).ConfigureAwait(false);
        }

        public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
        {
            var query = "INSERT INTO Categories (Name, Description) VALUES (@Name, @Description)";
            await _session.Connection.ExecuteAsync(
                new CommandDefinition(
                    query,
                    category,
                    _session.Transaction,
                    cancellationToken:cancellationToken
                    )
                ).ConfigureAwait(false);
        }

        public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE Categories SET Name = @Name, Description = @Description WHERE ID = @ID";
			await _session.Connection.ExecuteAsync(
				new CommandDefinition(
					query,
					category,
					_session.Transaction,
					cancellationToken: cancellationToken
					)
				).ConfigureAwait(false);
		}

        public async Task DeleteAsync(Category category, CancellationToken cancellationToken = default)
        {
            var query = "DELETE FROM Categories WHERE ID = @ID";
			await _session.Connection.ExecuteAsync(
				new CommandDefinition(
					query,
					new { category.ID },
					_session.Transaction,
					cancellationToken: cancellationToken
					)
				).ConfigureAwait(false);
		}

        public async Task<Category> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var query = "SELECT * FROM Categories WHERE Name = @name";
			return await _session.Connection.QueryFirstOrDefaultAsync<Category>(
				new CommandDefinition(
					query,
					new { Name = name },
					_session.Transaction,
					cancellationToken: cancellationToken
					)
				).ConfigureAwait(false);
		}
    }
}