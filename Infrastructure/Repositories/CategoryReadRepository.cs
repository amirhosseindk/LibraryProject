using Application.Patterns;
using Dapper;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
	public class CategoryReadRepository : ICategoryReadRepository
	{
		private readonly AppDbSession _session;

		public CategoryReadRepository(AppDbSession session)
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