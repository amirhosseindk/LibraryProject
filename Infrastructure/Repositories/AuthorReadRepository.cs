using Application.Patterns;
using Dapper;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
	public class AuthorReadRepository : IAuthorReadRepository
	{
		private readonly AppDbSession _session;

		public AuthorReadRepository(AppDbSession session)
		{
			_session = session;
		}

		public async Task<Author> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			var query = "SELECT * FROM Authors WHERE ID = @Id";
			var queryResult = await _session.Connection
				.QueryFirstOrDefaultAsync<Author>(
				new CommandDefinition(
					query,
					new { Id = id },
					_session.Transaction,
					cancellationToken: cancellationToken
				   )
				).ConfigureAwait(false);

			return queryResult;
		}

		public async Task<Author> GetByNameAsync(string lastName, CancellationToken cancellationToken = default)
		{
			var query = "SELECT * FROM Authors WHERE LastName = @LastName";
			var queryResult = await _session.Connection.QueryFirstOrDefaultAsync<Author>(
				new CommandDefinition(
					query,
					new { LastName = lastName },
					_session.Transaction,
					cancellationToken: cancellationToken
					)
				).ConfigureAwait(false);
			return queryResult;
		}

		public async Task<IEnumerable<Author>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var query = "SELECT * FROM Authors";
			return await _session.Connection.QueryAsync<Author>(
				new CommandDefinition(
					query,
					_session.Transaction,
					cancellationToken: cancellationToken
					)
				).ConfigureAwait(false);
		}
	}
}